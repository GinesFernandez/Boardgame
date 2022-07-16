using System.Windows;

namespace MineSweeper
{

    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ViewModelLocator.SetAndReg();
        }
    }
}
