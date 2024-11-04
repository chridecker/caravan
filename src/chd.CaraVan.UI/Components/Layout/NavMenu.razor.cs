using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace chd.CaraVan.UI.Components.Layout
{
    public partial class NavMenu : ComponentBase
    {
        [Inject] private INavigationHandler _navigationHandler { get; set; }
    
        private void GoHome()
        {
            this._navigationHandler.NavigateToRoot();
        }
    }
}