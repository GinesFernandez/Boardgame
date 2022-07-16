using MineSweeper.Services;
using MineSweeper.Controls;
using MineSweeper.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

namespace MineSweeper
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public static void SetAndReg()
        {
            var container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            container.RegisterType<CellViewModel>();
            container.RegisterType<BoardViewModel>();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<MineGameOrchestrator>();
        }
    }
}
