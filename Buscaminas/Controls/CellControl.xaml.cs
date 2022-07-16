using System.Windows;
using MineSweeper.Models;
using Common.Base;
using Common.Models;

namespace MineSweeper.Controls
{
    public abstract class CellControlMid : ControlBaseView<CellViewModel> { }

    public partial class CellControl
    {
        public delegate void Clicked(CellPosition pos);
        public event Clicked CellClicked;

        public static readonly DependencyProperty CellModelProperty =
            DependencyProperty.Register("CellModel", typeof(MineCellModel), typeof(CellControl), new PropertyMetadata(OnCellModelChanged));
        
        public MineCellModel CellModel
        {
            get { return (MineCellModel)GetValue(CellModelProperty); }
            set { SetValue(CellModelProperty, value); }
        }

        private static void OnCellModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var view = sender as CellControl;

            if (view == null) return;

            view.ViewModel.Cell = e.NewValue as MineCellModel;
        }

        public CellControl()
        {
            InitializeComponent();
        }

        private void Layoutroot_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CellClicked(ViewModel.Cell.Pos);
        }
    }
}
