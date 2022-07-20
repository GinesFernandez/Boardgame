using Damas.Models;
using Common.Base;

namespace Damas.Controls
{
    public class BoardViewModel : ViewModelBase
    {
        private DamasBoardModel _board;
        public DamasBoardModel Board
        {
            get => _board;
            set
            {
                if (_board == value) return;

                _board = value;
                RaisePropertyChanged();
            }
        }
    }
}
