using System.Windows.Media;
using Common.Models;

namespace Damas.Models
{
    public class DamasCellModel : CellModel
    {
        private readonly SolidColorBrush _whiteBrush = new SolidColorBrush(Colors.White);
        private readonly SolidColorBrush _blackBrush = new SolidColorBrush(Colors.Black);

        public DamasCellModel(CellPosition pos, CellState value) : base(pos)
        {
            Value = value;
            Image = Globals.QueenImg;
        }
        
        private CellState _value;
        public CellState Value
        {
            get => _value;
            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsEmpty));
                RaisePropertyChanged(nameof(IsWhite));
                RaisePropertyChanged(nameof(IsBlack));
                RaisePropertyChanged(nameof(IsQueen));
            }
        }

        public override SolidColorBrush Brush => (Pos.PosX + Pos.PosY) % 2 == 0 ?
            _whiteBrush
            : _blackBrush;

        public bool IsEmpty => Value == CellState.Empty;

        public bool IsQueen => Value == CellState.WhiteQueen || Value == CellState.BlackQueen;

        public bool IsWhite => Value == CellState.WhiteToken || Value == CellState.WhiteQueen;

        public bool IsBlack => Value == CellState.BlackToken || Value == CellState.BlackQueen;

    }
}