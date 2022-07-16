using Common.Base;
using Common.Models;
using MineSweeper.ViewModels;

namespace MineSweeper.Views
{
    public abstract class MainWindowMid : WindowBaseView<MainWindowViewModel> { }

    public partial class MainWindow
    {
        public override MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        private void BoardControl_CellClicked(CellPosition pos)
        {
            ViewModel.Orchestrator.CellClicked(pos.PosX, pos.PosY);
        }
    }
}
