using MineSweeper.Models;
using Common.Base;

namespace MineSweeper.Controls
{
    public class CellViewModel : ViewModelBase
    {
        private MineCellModel _cell;
        public MineCellModel Cell
        {
            get => _cell;
            set
            {
                if (_cell == value)
                    return;

                _cell = value;
                RaisePropertyChanged();
            }
        }
    }
}
