using ColorMemoryGame.Models;
using Common.Base;

namespace ColorMemoryGame.Controls
{
    public class BoardViewModel : ViewModelBase
    {
        private ColorBoardModel _board;
        public ColorBoardModel Board
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
