using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace chd.Caravan.Mobile.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     public partial class MainWindow : Window
    {
        protected override void OnInitialized(EventArgs e)
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.OnInitialized(e);
        }
        public void Reload() => this.blazorWebView.WebView.Reload();
    }
}