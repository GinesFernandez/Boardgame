using ColorMemoryGame.Models;
using Common.Base;

namespace ColorMemoryGame.Controls
{
    public class CellViewModel : ViewModelBase
    {
        private ColorCellModel _cell;
        public ColorCellModel Cell
        {
            get => _cell;
            set
            {
                if (_cell == value) return;

                _cell = value;
                RaisePropertyChanged();
            }
        }
    }
}
