using Android.Webkit;
using AndroidX.Activity.Result.Contract;
using AndroidX.Activity.Result;
using AndroidX.Core.Content;
using Android;
using AndroidX.Activity;
using Android.Content.PM;
using Android.App;

namespace chd.CaraVan.App.Platforms.Android
{
  internal class PermissionManagingBlazorWebChromeClient : WebChromeClient, IActivityResultCallback
    {
        // This class implements a permission requesting workflow that matches workflow recommended
        // by the official Android developer documentation.
        // See: https://developer.android.com/training/permissions/requesting#workflow_for_requesting_permissions
        // The current implementation supports camera permissions. To add your own,
        // update the s_rationalesByPermission dictionary to include your rationale for requiring the permission.
        // If necessary, you may need to also update s_requiredPermissionsByWebkitResource to define how a specific
        // Webkit resource maps to an Android permission.

        // In a real app, you would probably use more convincing rationales tailored toward what your app does.
        private const string CameraAccessRationale = "This app requires access to your camera. Please grant access to your camera when requested.";

        private static readonly Dictionary<string, string> s_rationalesByPermission = new()
        {
            [Manifest.Permission.Camera] = CameraAccessRationale
            // Add more rationales as you add more supported permissions.
        };

        private static readonly Dictionary<string, string[]> s_requiredPermissionsByWebkitResource = new()
        {
            [PermissionRequest.ResourceVideoCapture] = new[] { Manifest.Permission.Camera },
            // [PermissionRequest.ResourceAudioCapture] = new[] { Manifest.Permission.ModifyAudioSettings, Manifest.Permission.RecordAudio },
            // Add more Webkit resource -> Android permission mappings as needed.
        };

        private readonly WebChromeClient _blazorWebChromeClient;
        private readonly ComponentActivity _activity;
        private readonly ActivityResultLauncher _requestPermissionLauncher;

        private Action<bool>? _pendingPermissionRequestCallback;

        public PermissionManagingBlazorWebChromeClient(WebChromeClient blazorWebChromeClient, ComponentActivity activity)
        {
            this._blazorWebChromeClient = blazorWebChromeClient;
            this._activity = activity;
            this._requestPermissionLauncher = this._activity.RegisterForActivityResult(new ActivityResultContracts.RequestPermission(), this);
        }

        public override void OnCloseWindow(global::Android.Webkit.WebView window)
        {
            this._blazorWebChromeClient.OnCloseWindow(window);
            this._requestPermissionLauncher.Unregister();
            base.OnCloseWindow(window);
        }

        public override void OnPermissionRequest(PermissionRequest? request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            if (request.GetResources() is not { } requestedResources)
            {
                request.Deny();
                return;
            }

            this.RequestAllResources(requestedResources, grantedResources =>
            {
                if (grantedResources.Count == 0)
                {
                    request.Deny();
                }
                else
                {
                    request.Grant(grantedResources.ToArray());
                }
            });
        }

        private void RequestAllResources(Memory<string> requestedResources, Action<List<string>> callback)
        {
            if (requestedResources.Length == 0)
            {
                // No resources to request - invoke the callback with an empty list.
                callback(new());
                return;
            }

            var currentResource = requestedResources.Span[0];
            var requiredPermissions = s_requiredPermissionsByWebkitResource.GetValueOrDefault(currentResource, Array.Empty<string>());

            this.RequestAllPermissions(requiredPermissions, isGranted =>
            {
                // Recurse with the remaining resources. If the first resource was granted, use a modified callback
                // that adds the first resource to the granted resources list.
                this.RequestAllResources(requestedResources[1..], !isGranted ? callback : grantedResources =>
                {
                    grantedResources.Add(currentResource);
                    callback(grantedResources);
                });
            });
        }

        private void RequestAllPermissions(Memory<string> requiredPermissions, Action<bool> callback)
        {
            if (requiredPermissions.Length == 0)
            {
                // No permissions left to request - success!
                callback(true);
                return;
            }

            this.RequestPermission(requiredPermissions.Span[0], isGranted =>
            {
                if (isGranted)
                {
                    // Recurse with the remaining permissions.
                    this.RequestAllPermissions(requiredPermissions[1..], callback);
                }
                else
                {
                    // The first required permission was not granted. Fail now and don't attempt to grant
                    // the remaining permissions.
                    callback(false);
                }
            });
        }

        private void RequestPermission(string permission, Action<bool> callback)
        {
            // This method implements the workflow described here:
            // https://developer.android.com/training/permissions/requesting#workflow_for_requesting_permissions

            if (ContextCompat.CheckSelfPermission(this._activity, permission) == Permission.Granted)
            {
                callback.Invoke(true);
            }
            else if (this._activity.ShouldShowRequestPermissionRationale(permission) && s_rationalesByPermission.TryGetValue(permission, out var rationale))
            {
                new AlertDialog.Builder(this._activity)
                    .SetTitle("Enable app permissions")!
                    .SetMessage(rationale)!
                    .SetNegativeButton("No thanks", (_, _) => callback(false))!
                    .SetPositiveButton("Continue", (_, _) => this.LaunchPermissionRequestActivity(permission, callback))!
                    .Show();
            }
            else
            {
                this.LaunchPermissionRequestActivity(permission, callback);
            }
        }

        private void LaunchPermissionRequestActivity(string permission, Action<bool> callback)
        {
            if (this._pendingPermissionRequestCallback is not null)
            {
                throw new InvalidOperationException("Cannot perform multiple permission requests simultaneously.");
            }

            this._pendingPermissionRequestCallback = callback;
            this._requestPermissionLauncher.Launch(permission);
        }

        void IActivityResultCallback.OnActivityResult(Java.Lang.Object isGranted)
        {
            var callback = this._pendingPermissionRequestCallback;
            this._pendingPermissionRequestCallback = null;
            callback?.Invoke((bool)isGranted);
        }
    }
}
