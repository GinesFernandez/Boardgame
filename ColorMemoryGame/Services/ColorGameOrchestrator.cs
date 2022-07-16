using System;
using System.Windows.Threading;
using ColorMemoryGame.Models;
using Common;

namespace ColorMemoryGame.Services
{
    public class ColorGameOrchestrator : GameOrchestrator
    {
        private ColorCellModel _lastCard;
        private ColorCellModel _cardFailed;
        private int _cardsMatched;
        private DispatcherTimer _timerFails;

        public ColorGameOrchestrator() : base(Globals.Rows, Globals.Columns, (int)GameStates.NocardSelected)
        {
            Reset();
        }

        public GameStates State => (GameStates)GameState;

        public override bool IsWin => State == GameStates.EndedGood;

        public override void Reset()
        {
            Reset(Rows, Columns);
        }

        public override void Reset(int rows, int columns, object param = null)
        {
            Rows = rows;
            Columns = columns;
            _lastCard = null;
            _cardsMatched = Moves = 0;
            GameState = (int)GameStates.NocardSelected;
            RaisePropertyChanged(nameof(IsWin));

            _timerFails = new DispatcherTimer { Interval = new TimeSpan(10000000) };
            _timerFails.Tick -= timerFail_Tick;
            _timerFails.Tick += timerFail_Tick;

            Board = new ColorBoardModel(Rows, Columns);
        }

        public override void CellClicked(int posx, int posy)
        {
            if (State == GameStates.EndedGood
                || State == GameStates.ShowingFail)
            {
                return;
            }

            var card = (ColorCellModel)Board.CellsMatrix[posy][posx];

            if (card.IsWin || card.IsSelected)
            {
                return;
            }

            if (State == GameStates.NocardSelected)
            {
                card.IsSelected = true;
                GameState = (int)GameStates.Onecardselected;
                _lastCard = card;
                return;
            }

            if (State == GameStates.Onecardselected)
            {
                card.IsSelected = true;
                Moves++;

                if (card.Color == _lastCard.Color) //Math!
                {
                    _cardsMatched++;
                    _lastCard.IsWin = true;
                    card.IsWin = true;

                    if (_cardsMatched == Rows * Columns / 2)
                    {
                        GameState = (int)GameStates.EndedGood;
                        RaisePropertyChanged(nameof(IsWin));
                    }
                    else
                    {
                        GameState = (int)GameStates.NocardSelected;
                    }

                    _lastCard = null;
                }
                else //Fail!
                {
                    GameState = (int)GameStates.ShowingFail;
                    _cardFailed = card;
                    _timerFails.Start();
                }

                return;
            }
        }

        private void timerFail_Tick(object sender, EventArgs e)
        {
            _timerFails.Stop();

            _cardFailed.IsSelected = false;
            _lastCard.IsSelected = false;
            _lastCard = _cardFailed = null;
            GameState = (int)GameStates.NocardSelected;
        }
    }
}
