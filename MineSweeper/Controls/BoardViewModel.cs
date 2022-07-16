using MineSweeper.Models;
using Common.Base;

namespace MineSweeper.Controls
{
    public class BoardViewModel : ViewModelBase
    {
        private MineBoardModel _board;
        public MineBoardModel Board
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
