using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Components.Layout
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
