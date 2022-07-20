namespace Common
{
    public abstract class GameOrchestrator2p : GameOrchestrator
    {
        protected GameOrchestrator2p(int rows, int columns, int initialState) : base(rows, columns, initialState)
        {
        }

        private int _scoreP1;
        public int ScoreP1
        {
            get => _scoreP1;
            protected set
            {
                if (_scoreP1 == value)
                    return;

                _scoreP1 = value;
                RaisePropertyChanged();
            }
        }

        private int _scoreP2;
        public int ScoreP2
        {
            get => _scoreP2;
            protected set
            {
                if (_scoreP2 == value)
                    return;

                _scoreP2 = value;
                RaisePropertyChanged();
            }
        }

        private int _movesP1;
        public int MovesP1
        {
            get => _movesP1;
            set
            {
                if (_movesP1 == value)
                    return;

                _movesP1 = value;
                RaisePropertyChanged();
            }
        }

        private int _movesP2;

        public int MovesP2
        {
            get => _movesP2;
            set
            {
                if (_movesP2 == value)
                    return;

                _movesP2 = value;
                RaisePropertyChanged();
            }
        }

        public abstract bool IsP1Win { get; }
        public abstract bool IsP2Win { get; }
    }
}
