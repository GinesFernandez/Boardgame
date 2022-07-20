using Common.Base;
using Common.Models;
using Damas.ViewModels;

namespace Damas.Views
{
    public abstract class MainWindowMid : WindowBaseView<MainWindowViewModel> { }

    public partial class MainWindow
    {
        public override MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        private void BoardControl_CellClicked(CellPosition pos)
        {
            ViewModel.Orchestrator.CellClicked(pos.PosX, pos.PosY);
        }
    }
}
