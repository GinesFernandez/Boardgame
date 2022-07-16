using System.Windows;
using ColorMemoryGame.Models;
using Common.Base;
using Common.Models;

namespace ColorMemoryGame.Controls
{
    public abstract class CellControlMid : ControlBaseView<CellViewModel> { }

    public partial class CellControl
    {
        public delegate void Clicked(CellPosition pos);
        public event Clicked CellClicked;

        public static readonly DependencyProperty CellModelProperty =
            DependencyProperty.Register("CellModel", typeof(ColorCellModel), typeof(CellControl), new PropertyMetadata(OnCellModelChanged));
        
        public ColorCellModel CellModel
        {
            get { return (ColorCellModel)GetValue(CellModelProperty); }
            set { SetValue(CellModelProperty, value); }
        }

        private static void OnCellModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var view = sender as CellControl;

            if (view == null)
                return;

            view.ViewModel.Cell = e.NewValue as ColorCellModel;
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
