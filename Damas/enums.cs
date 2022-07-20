namespace Damas
{
    public enum GameStates
    {
        NotStarted = 0,
        EndedWinWhite = 1,
        EndedWinBlack = 2,
        WaitingMoveWhite = 3,
        WaitingMoveBlack = 4,
        WhiteSelected = 5,
        BlackSelected = 6
    }

    public enum CellState
    {
        Empty = 0,
        WhiteToken = 1,
        BlackToken = 2,
        WhiteQueen = 3,
        BlackQueen = 4,
    }
}
