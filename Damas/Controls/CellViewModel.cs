using Damas.Models;
using Common.Base;

namespace Damas.Controls
{
    public class CellViewModel : ViewModelBase
    {
        private DamasCellModel? _cell;
        public DamasCellModel? Cell
        {
            get => _cell;
            set
            {
                if (_cell == value)
                {
                    return;
                }

                _cell = value;
                RaisePropertyChanged();
            }
        }
    }
}
