using System.Windows.Media;
using Common.Models;

namespace ColorMemoryGame.Models
{
    public class ColorCellModel : CellModel
    {
        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                _color = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Brush));
            }
        }

        public override SolidColorBrush Brush => new SolidColorBrush(_color);

        private bool _isWin;
        public bool IsWin
        {
            get => _isWin;
            set
            {
                if (_isWin == value)
                    return;

                _isWin = value;
                RaisePropertyChanged();

                IsBack = !_isWin;
            }
        }
        
        public ColorCellModel(CellPosition pos, Color color) : base(pos)
        {
            Color = color;
            Image = Globals.CellBack;
        }
    }
}