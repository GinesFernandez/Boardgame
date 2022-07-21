using System;
using System.Collections.Generic;
using Common;
using Common.Models;
using Damas.Models;

namespace Damas.Services
{
    public class DamasGameOrchestrator : GameOrchestrator2p
    {
        private DamasCellModel? _lastCell;

        public DamasGameOrchestrator() : base(Globals.Rows, Globals.Columns, (int)GameStates.WaitingMoveWhite)
        {
            Reset();
        }

        public GameStates State => (GameStates)GameState;

        public override bool IsWin => State == GameStates.EndedWinWhite || State == GameStates.EndedWinBlack;
        public override bool IsP1Win => State == GameStates.EndedWinWhite;
        public override bool IsP2Win => State == GameStates.EndedWinBlack;

        public override void Reset()
        {
            Reset(Rows, Columns);
        }

        public override void Reset(int rows, int columns, object? param = null)
        {
            Rows = rows;
            Columns = columns;
            Moves = MovesP1 = MovesP2 = ScoreP1 = ScoreP2 = 0;
            GameState = (int)GameStates.WaitingMoveWhite;

            RefreshWin();
            
            Board = new DamasBoardModel(Rows, Columns);
        }

        public override void CellClicked(int posx, int posy)
        {
            if (State == GameStates.EndedWinWhite
                || State == GameStates.EndedWinBlack)
            {
                return;
            }

            var cell = (DamasCellModel)Board.CellsMatrix[posy][posx];
            var isCellSelected = cell.IsSelected;

            switch (State)
            {
                case GameStates.WaitingMoveWhite:
                    if (!cell.IsWhite) //white must be selected
                    {
                        return;
                    }

                    if (!isCellSelected) //selecting white
                    {
                        cell.IsSelected = true;
                        _lastCell = cell;
                        GameState = (int)GameStates.WhiteSelected;
                        return;
                    }

                    break;
                case GameStates.WaitingMoveBlack:
                    if (!cell.IsBlack) //black must be selected
                    {
                        return;
                    }

                    if (!isCellSelected) //selecting black
                    {
                        cell.IsSelected = true;
                        _lastCell = cell;
                        GameState = (int)GameStates.BlackSelected;
                        return;
                    }
                    break;
                case GameStates.WhiteSelected:
                    if (_lastCell == null)
                    {
                        throw new InvalidOperationException("_lastCell cannot be null");
                    }

                    if (isCellSelected) //undo selection
                    {
                        cell.IsSelected = false;
                        _lastCell = null;
                        GameState = (int)GameStates.WaitingMoveWhite;
                        return;
                    }

                    if (cell.Value != CellState.Empty) //empty cell must be selected
                    {
                        return;
                    }

                    //check if empty cell selected is valid:
                    if (!_lastCell.IsQueen)
                    {
                        if (AreAdjacentsDown(_lastCell!.Pos, cell.Pos)) //moving forward (whites go down, blacks go up)
                        {
                            WhiteMove(cell);
                            return;
                        }
                        if (IsWhiteEating(_lastCell.Pos, cell.Pos, out var eaten)) // eating black token
                        {
                            WhiteEat(cell, eaten!);
                        }
                    }
                    else
                    {
                        if (IsValidBishopMove(_lastCell.Pos, cell.Pos, true, out var eaten))
                        {
                            if (eaten == null)
                            {
                                WhiteMove(cell);
                                return;
                            }

                            WhiteEat(cell, eaten!);
                        }
                    }

                    CheckVictoryCondition();
                    break;
                case GameStates.BlackSelected:
                    if (_lastCell == null)
                    {
                        throw new InvalidOperationException("_lastCell cannot be null");
                    }

                    if (isCellSelected) //undo selection
                    {
                        cell.IsSelected = false;
                        _lastCell = null;
                        GameState = (int)GameStates.WaitingMoveBlack;
                        return;
                    }

                    if (cell.Value != CellState.Empty) //empty cell must be selected
                    {
                        return;
                    }

                    //check if empty cell selected is valid:
                    if (!_lastCell.IsQueen)
                    {
                        if (AreAdjacentsUp(_lastCell!.Pos, cell.Pos)) //moving forward (whites go down, blacks go up)
                        {
                            BlackMove(cell);
                            return;
                        }
                        if (IsBlackEating(_lastCell.Pos, cell.Pos, out var eaten)) //eating white token
                        {
                            BlackEat(cell, eaten!);
                        }
                    }
                    else
                    {
                        if (IsValidBishopMove(_lastCell.Pos, cell.Pos, false, out var eaten))
                        {
                            if (eaten == null)
                            {
                                BlackMove(cell);
                                return;
                            }

                            BlackEat(cell, eaten!);
                        }
                    }

                    CheckVictoryCondition();
                    break;
            }
        }

        /// <summary>
        /// Check if move to pos2 from pos1 is valid (not eating) for whites (going down)
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool AreAdjacentsDown(CellPosition pos1, CellPosition pos2)
        {
            if (Math.Abs(pos1.PosX - pos2.PosX) != 1 || pos2.PosY - pos1.PosY != 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if move to pos2 from pos1 is valid (not eating) for black (going up)
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool AreAdjacentsUp(CellPosition pos1, CellPosition pos2)
        {
            if (Math.Abs(pos1.PosX - pos2.PosX) != 1 || pos1.PosY - pos2.PosY != 1)
            {
                return false;
            }

            return true;
        }

        private bool IsWhiteEating(CellPosition pos1, CellPosition pos2, out DamasCellModel? eaten) //for white (going down)
        {
            if (Math.Abs(pos1.PosX - pos2.PosX) != 2 || pos2.PosY - pos1.PosY != 2)
            {
                eaten = null;
                return false;
            }

            eaten = (DamasCellModel)Board.CellsMatrix[pos1.PosY + 1][pos1.PosX + (pos1.PosX < pos2.PosX ? 1 : -1)];

            if (!eaten.IsBlack) //eaten token must be black
            {
                eaten = null;
                return false;
            }

            return true;
        }

        private bool IsBlackEating(CellPosition pos1, CellPosition pos2, out DamasCellModel? eaten) //for black (going up)
        {
            if (Math.Abs(pos1.PosX - pos2.PosX) != 2 || pos1.PosY - pos2.PosY != 2)
            {
                eaten = null;
                return false;
            }

            eaten = (DamasCellModel)Board.CellsMatrix[pos1.PosY - 1][pos1.PosX + (pos1.PosX < pos2.PosX ? 1 : -1)];

            if (!eaten.IsWhite) //eaten token must be white
            {
                eaten = null;
                return false;
            }

            return true;
        }

        private bool IsValidBishopMove(CellPosition pos1, CellPosition pos2, bool isWhite, out DamasCellModel? eaten)
        {
            //move must be diagonal:
            if (Math.Abs(pos1.PosX - pos2.PosX) != Math.Abs(pos1.PosY - pos2.PosY))
            {
                eaten = null;
                return false;
            }

            var isGoingDown = pos1.PosY < pos2.PosY;
            var isGoingRight = pos1.PosX < pos2.PosX;
            var cellsMoved = Math.Abs(pos1.PosX - pos2.PosX);

            //populate cells passed in the movement:
            List<DamasCellModel> cellsPassed = new List<DamasCellModel>();
            for (int i = 1; i <= cellsMoved; i++)
            {
                cellsPassed.Add(
                    (DamasCellModel)
                        Board.CellsMatrix[pos1.PosY + (isGoingDown ? i : -i)]
                                         [pos1.PosX + (isGoingRight ? i : -i)]
                );
            }

            //check cellsPassed don't have obstacles in the middle:
            if (cellsMoved > 2)
            {
                foreach (var cell in cellsPassed.GetRange(0, cellsPassed.Count - 2))
                {
                    if (!cell.IsEmpty)
                    {
                        eaten = null;
                        return false;
                    }
                }
            }

            //Check if a token was eaten and it's valid: 
            eaten = cellsMoved > 1 ? cellsPassed[cellsPassed.Count - 2] : null;

            if (eaten != null)
            {
                //eaten token must be contrary color:
                if (eaten.IsWhite && isWhite || eaten.IsBlack && !isWhite)
                {
                    eaten = null;
                    return false;
                }

                if (eaten.IsEmpty) //not token eaten
                {
                    eaten = null;
                }
            }

            return true;
        }

        private void WhiteMove(DamasCellModel cell)
        {
            if (_lastCell == null)
            {
                throw new InvalidOperationException("_lastCell cannot be null");
            }

            cell.Value = cell.Pos.PosY == Rows - 1 || _lastCell.IsQueen ? CellState.WhiteQueen : CellState.WhiteToken;
            _lastCell.Value = CellState.Empty;
            _lastCell.IsSelected = false;
            GameState = (int)GameStates.WaitingMoveBlack;
            Moves++;
            MovesP1++;
            _lastCell = null;
        }

        private void BlackMove(DamasCellModel cell)
        {
            if (_lastCell == null)
            {
                throw new InvalidOperationException("_lastCell cannot be null");
            }

            cell.Value = cell.Pos.PosY == 0 || _lastCell.IsQueen ? CellState.BlackQueen : CellState.BlackToken;
            _lastCell.Value = CellState.Empty;
            _lastCell.IsSelected = false;
            GameState = (int)GameStates.WaitingMoveWhite;
            MovesP2++;
            _lastCell = null;
        }

        private void WhiteEat(DamasCellModel cell, DamasCellModel eaten)
        {
            if (cell.Value != CellState.Empty)
            {
                throw new InvalidOperationException("cell must be a empty");
            }
            if (eaten.Value != CellState.BlackToken && eaten.Value != CellState.BlackQueen)
            {
                throw new InvalidOperationException("eaten must be a black token");
            }

            eaten!.Value = CellState.Empty;
            ScoreP1++;
            WhiteMove(cell);
        }

        private void BlackEat(DamasCellModel cell, DamasCellModel eaten)
        {
            if (cell.Value != CellState.Empty)
            {
                throw new InvalidOperationException("cell must be empty");
            }
            if (eaten.Value != CellState.WhiteToken && eaten.Value != CellState.WhiteQueen)
            {
                throw new InvalidOperationException("eaten must be a white token");
            }

            eaten!.Value = CellState.Empty;
            ScoreP2++;
            BlackMove(cell);
        }

        private void RefreshWin()
        {
            RaisePropertyChanged(nameof(IsWin));
            RaisePropertyChanged(nameof(IsP1Win));
            RaisePropertyChanged(nameof(IsP2Win));
        }

        private void CheckVictoryCondition()
        {
            if (ScoreP1 == Columns / 2 * 3)
            {
                GameState = (int)GameStates.EndedWinWhite;
                RefreshWin();
            }
            else if (ScoreP2 == Columns / 2 * 3)
            {
                GameState = (int)GameStates.EndedWinBlack;
                RefreshWin();
            }
        }
    }
}
