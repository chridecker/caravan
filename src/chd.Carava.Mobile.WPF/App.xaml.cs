using chd.Base.UI.WPF.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace chd.Caravan.Mobile.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IInitComponents
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public App(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            this._serviceProvider = serviceProvider;
            this._hostApplicationLifetime = hostApplicationLifetime;
            Resources.Add("services", this._serviceProvider);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this._hostApplicationLifetime.StopApplication();
            base.OnExit(e);
        }

        internal App()
        {
        }
    }
}
