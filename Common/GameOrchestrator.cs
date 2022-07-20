using Common.Base;
using Common.Models;

namespace Common
{
    public abstract class GameOrchestrator : ViewModelBase
    {
        public GameOrchestrator(int rows, int columns, int initialState)
        {
            GameState = initialState;
            _rows = rows;
            _columns = columns;
        }

        private BoardModel _board;
        public BoardModel Board
        {
            get => _board;
            protected set
            {
                if (_board == value) return;

                _board = value;
                RaisePropertyChanged();
            }
        }

        private int _rows;
        public int Rows
        {
            get => _rows;
            protected set
            {
                if (_rows == value)
                    return;

                _rows = value;
                RaisePropertyChanged();
            }
        }

        private int _columns;
        public int Columns
        {
            get => _columns;
            protected set
            {
                if (_columns == value)
                    return;

                _columns = value;
                RaisePropertyChanged();
            }
        }

        private int _moves;
        public int Moves
        {
            get => _moves;
            protected set
            {
                if (_moves == value)
                    return;

                _moves = value;
                RaisePropertyChanged();
            }
        }

        private int _gameState;
        public int GameState
        {
            get => _gameState;
            protected set
            {
                if (_gameState == value)
                    return;

                _gameState = value;
                RaisePropertyChanged();
            }
        }

        public abstract bool IsWin { get; }

        public abstract void Reset();

        public abstract void Reset(int rows, int columns, object args = null);

        public abstract void CellClicked(int posx, int posy);
    }
}
