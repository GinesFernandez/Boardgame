using Damas.Services;
using Damas.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;
using Damas.Controls;

namespace Damas
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
            container.RegisterType<DamasGameOrchestrator>();
        }
    }
}
