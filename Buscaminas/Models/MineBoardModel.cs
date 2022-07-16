using System;
using System.Collections.Generic;
using Common.Base;
using Common.Models;

namespace MineSweeper.Models
{
    public class MineBoardModel : BoardModel
    {
        private static Random _random1 = new Random(1);
        private static object _syncObj = new object();

        private int _mines;
        public int Mines
        {
            get => _mines;

            set
            {
                if (_mines == value) return;

                _mines = value;
                GenerateCells();
                RaisePropertyChanged();
            }
        }

        public MineBoardModel(int rows = 10, int columns = 10, int mines = 20)
        {
            if (rows * columns < 2 || mines >= rows * columns)
            {
                throw new InvalidOperationException("Too many mines!");
            }

            Rows = rows;
            Columns = columns;
            _mines = mines;

            GenerateCells();
        }

        protected override void GenerateCells()
        {
            var mineValues = GenerateRandomMines(Rows, Columns, _mines);

            CellsMatrix = new List<List<CellModel>>();

            for (int i = 0; i < Rows; i++)
            {
                var r = new List<CellModel>();
                CellsMatrix.Add(r);

                for (int j = 0; j < Columns; j++)
                {
                    r.Add(new MineCellModel(new CellPosition(j, i), mineValues[i, j]));
                }
            }

            RaisePropertyChanged(nameof(CellsMatrix));
        }

        private int?[,] GenerateRandomMines(int rows, int cols, int mines)
        {
            int?[,] result = new int?[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = 0;
                }
            }

            lock (_syncObj)
            {
                //fill the mines
                for (int i = 0; i < mines; i++)
                {
                    bool notEnc = true;
                    while (notEnc)
                    {
                        int r = _random1.Next(0, rows);
                        int c = _random1.Next(0, cols);

                        if (result[r, c] != null)
                        {
                            result[r, c] = null;
                            notEnc = false;
                        }
                    }
                }
            }

            //fill not mines
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (result[i, j] == null)
                        continue;

                    result[i, j] = CountMinesAround(i, j, rows, cols, result);
                }
            }

            return result;
        }

        private int CountMinesAround(int i, int j, int rows, int cols, int?[,] matrix)
        {
            int result = 0;

            //check left
            if (j > 0)
                if (matrix[i, j - 1] == null)
                    result++;

            //check right
            if (j < cols - 1)
                if (matrix[i, j + 1] == null)
                    result++;

            //check up
            if (i > 0)
                if (matrix[i - 1, j] == null)
                    result++;

            //check down
            if (i < rows - 1)
                if (matrix[i + 1, j] == null)
                    result++;

            //check leftup
            if (j > 0 && (i > 0))
                if (matrix[i - 1, j - 1] == null)
                    result++;

            //check leftdown
            if (j > 0 && i < rows - 1)
                if (matrix[i + 1, j - 1] == null)
                    result++;

            //check rightup
            if (j < cols - 1 && i > 0)
                if (matrix[i - 1, j + 1] == null)
                    result++;

            //check rightdown
            if (j < cols - 1 && i < rows - 1)
                if (matrix[i + 1, j + 1] == null)
                    result++;

            return result;
        }
    }
}