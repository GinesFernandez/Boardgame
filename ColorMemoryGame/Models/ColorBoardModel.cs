using System;
using System.Collections.Generic;
using System.Windows.Media;
using Common.Base;
using Common.Models;

namespace ColorMemoryGame.Models
{
    public class ColorBoardModel : BoardModel
    {
        private static Random _random1 = new Random(1);
        private static Random _random2 = new Random(10);
        private static Random _random3 = new Random(100);
        private static object _syncObj = new object();

        public ColorBoardModel(int rows = 4, int columns = 4) : base(rows, columns)
        {
            if (rows * columns % 2 != 0 || rows * columns < 2)
            {
                throw new InvalidOperationException("Total number of cards must be pair");
            }
        }

        protected override void GenerateCells()
        {
            var colors = GenerateRandomColors(Rows, Columns);

            CellsMatrix = new List<List<CellModel>>();

            for (int i = 0; i < Rows; i++)
            {
                var r = new List<CellModel>();
                CellsMatrix.Add(r);

                for (int j = 0; j < Columns; j++)
                {
                    r.Add(new ColorCellModel(new CellPosition(j, i), colors[i * Columns + j]));
                }
            }

            RaisePropertyChanged(nameof(CellsMatrix));
        }

        private Color[] GenerateRandomColors(int rows, int cols)
        {
            var total = rows * cols;
            Color[] result = new Color[total];

            Color color;
            Color defColor = new Color();
            int count = 0;
            int i = 0;
            int pos = 0;
            while (count < total / 2)
            {
                if (result[i] == defColor)
                {
                    lock (_syncObj)
                    {
                        pos = _random3.Next(total);
                        while (result[pos] != defColor && pos != i)
                        {
                            pos = _random2.Next(total);
                        }

                        var R = (byte)_random1.Next(256);
                        var G = (byte)_random2.Next(256);
                        var B = (byte)_random3.Next(256);

                        color = Color.FromArgb(255, R, G, B);
                    }

                    result[i] = color;
                    result[pos] = color;
                    count++;
                }
                i++;
            }

            for (i = 0; i < total; i++)
            {
                if (result[i] == defColor)
                {
                    return GenerateRandomColors(rows, cols);
                }
            }

            return result;
        }
    }
}