using ColorMemoryGame.Base;
using System.Windows;

namespace ColorMemoryGame
{

    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ViewModelLocator.SetAndReg();
        }
    }
}
