using System.Windows;
using Damas.Models;
using Common.Base;
using Common.Models;
using Damas.Controls;

namespace Damas.Controls
{
    public abstract class CellControlMid : ControlBaseView<CellViewModel> { }

    public partial class CellControl
    {
        public delegate void Clicked(CellPosition pos);
        public event Clicked CellClicked;

        public static readonly DependencyProperty CellModelProperty =
            DependencyProperty.Register("CellModel", typeof(DamasCellModel), typeof(CellControl), new PropertyMetadata(OnCellModelChanged));
        
        public DamasCellModel CellModel
        {
            get { return (DamasCellModel)GetValue(CellModelProperty); }
            set { SetValue(CellModelProperty, value); }
        }

        private static void OnCellModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var view = sender as CellControl;

            if (view == null) return;

            view.ViewModel.Cell = e.NewValue as DamasCellModel;
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
