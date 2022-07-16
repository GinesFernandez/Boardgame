using Common;
using MineSweeper.Models;

namespace MineSweeper.Services
{
    public class MineGameOrchestrator : GameOrchestrator
    {
        private int _mines;

        public MineGameOrchestrator() : base(Globals.Rows, Globals.Columns, (int)GameStates.WaitingMove)
        {
            _mines = Globals.Mines;
            Reset();
        }

        public GameStates State => (GameStates)GameState; 

        public override bool IsWin => State == GameStates.EndedGood;
        public bool IsFail => State == GameStates.EndedBad;
        public bool IsFinish => IsWin || IsFail;

        public override void Reset()
        {
            Reset(Rows, Columns, _mines);
        }

        public override void Reset(int rows, int columns, object mines)
        {
            Rows = rows;
            Columns = columns;
            _mines = (int)mines;
            Moves = 0;
            GameState = (int)GameStates.WaitingMove;
            RaisePropertyChanged(nameof(IsWin));
            RaisePropertyChanged(nameof(IsFail));
            RaisePropertyChanged(nameof(IsFinish));

            Board = new MineBoardModel(Rows, Columns, _mines);
        }

        public override void CellClicked(int posx, int posy)
        {
            if (State == GameStates.EndedGood
                || State == GameStates.EndedBad)
            {
                return;
            }

            var mineCell = (MineCellModel)Board.CellsMatrix[posy][posx];

            if (!mineCell.IsBack) return;

            if (State == GameStates.WaitingMove)
            {
                mineCell.IsBack = false;
                Moves++;

                if (mineCell.IsMine)
                {
                    GameState = (int)GameStates.EndedBad;
                    RaisePropertyChanged(nameof(IsFail));
                    RaisePropertyChanged(nameof(IsFinish));
                    TurnAll();
                    return;
                }

                if (Moves + _mines >= Rows * Columns)
                {
                    GameState = (int)GameStates.EndedGood;
                    RaisePropertyChanged(nameof(IsWin));
                    RaisePropertyChanged(nameof(IsFinish));
                    TurnAll();
                    return;
                }

                if (mineCell.Value == 0)
                {
                    //Open all 0 cells arround:
                    //check left
                    if (posx > 0)
                    {
                        if (((MineCellModel)Board.CellsMatrix[posy][posx - 1]).Value <= 1)
                        {
                            CellClicked(posx - 1, posy);
                        }
                    }

                    //check right
                    if (posx < Columns - 1)
                    {
                        if (((MineCellModel)Board.CellsMatrix[posy][posx + 1]).Value <= 1)
                        {
                            CellClicked(posx + 1, posy);
                        }
                    }

                    //check up
                    if (posy > 0)
                    {
                        if (((MineCellModel)Board.CellsMatrix[posy - 1][posx]).Value <= 1)
                        {
                            CellClicked(posx, posy - 1);
                        }
                    }

                    //check down
                    if (posy < Rows - 1)
                    {
                        if (((MineCellModel)Board.CellsMatrix[posy + 1][posx]).Value <= 1)
                        {
                            CellClicked(posx, posy + 1);
                        }
                    }

                    return;
                }
            }
        }

        private void TurnAll()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Board.CellsMatrix[i][j].IsBack = false;
                }
            }
        }
    }
}
