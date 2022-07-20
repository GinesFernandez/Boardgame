using System;
using System.Collections.Generic;
using Common.Models;

namespace Damas.Models
{
    public class DamasBoardModel : BoardModel
    {
        public DamasBoardModel(int rows = 8, int columns = 8) : base(rows, columns)
        {
            if (rows < 8 || columns < 4)
            {
                throw new InvalidOperationException("not valid number of rows/columns");
            }
        }

        protected override void GenerateCells()
        {
            CellsMatrix = new List<List<CellModel>>();

            for (int i = 0; i < Rows; i++)
            {
                var r = new List<CellModel>();
                CellsMatrix.Add(r);

                for (int j = 0; j < Columns; j++)
                {
                    var cellState = CellState.Empty;
                    if ((j + i) % 2 == 0)
                    {
                        if (i < 3)
                        {
                            cellState = CellState.WhiteToken;
                        }
                        else if (i > Rows - 4)
                        {
                            cellState = CellState.BlackToken;
                        }
                    }

                    r.Add(new DamasCellModel(new CellPosition(j, i), cellState));
                }
            }

            RaisePropertyChanged(nameof(CellsMatrix));
        }
    }
}