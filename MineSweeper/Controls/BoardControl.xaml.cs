using System.Windows;
using System.Windows.Controls;
using Common.Base;
using Common.Models;
using MineSweeper.Models;

namespace MineSweeper.Controls
{
    public abstract class BoardControlMid : ControlBaseView<BoardViewModel> { }

    public partial class BoardControl
    {
        public delegate void Clicked(CellPosition pos);
        public event Clicked CellClicked;

        public static readonly DependencyProperty BoardModelProperty =
            DependencyProperty.Register("BoardModel", typeof(MineBoardModel), typeof(BoardControl), new PropertyMetadata(OnBoardModelChanged));

        public MineBoardModel BoardModel
        {
            get { return (MineBoardModel)GetValue(BoardModelProperty); }
            set { SetValue(BoardModelProperty, value); }
        }

        private static void OnBoardModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var view = sender as BoardControl;

            if (view == null) return;

            view.ViewModel.Board = e.NewValue as MineBoardModel;

            view.GenerateCells();
        }

        private void GenerateCells()
        {
            gridBoard.Children.Clear();
            gridBoard.ColumnDefinitions.Clear();
            gridBoard.RowDefinitions.Clear();

            for (int i = 0; i < BoardModel.Columns; i++)
                gridBoard.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < BoardModel.Rows; i++)
                gridBoard.RowDefinitions.Add(new RowDefinition());

            
            for (int i = 0; i < BoardModel.Rows; i++)
            {
                for (int j = 0; j < BoardModel.Columns; j++)
                {
                    var control = new CellControl();
                    control.CellModel = (MineCellModel)BoardModel.CellsMatrix[i][j];
                    control.CellClicked += Control_CellClicked;

                    Grid.SetRow(control, i);
                    Grid.SetColumn(control, j);
                    gridBoard.Children.Add(control);
                }
            }
        }
        
        public BoardControl()
        {
            InitializeComponent();
        }

        private void Control_CellClicked(CellPosition pos)
        {
            CellClicked(pos);
        }
    }
}
