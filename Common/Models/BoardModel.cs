using System.Collections.Generic;
using Common.Base;

namespace Common.Models
{
    public abstract class BoardModel : ModelBase
    {
        private int _rows;
        public int Rows
        {
            get => _rows;

            protected set
            {
                if (_rows == value) return;

                _rows = value;
                GenerateCells();
                RaisePropertyChanged();
            }
        }

        private int _columns;
        public int Columns
        {
            get => _columns;

            protected set
            {
                if (_columns == value) return;

                _columns = value;
                GenerateCells();
                RaisePropertyChanged();
            }
        }

        private List<List<CellModel>> _cellsMatrix;
        public List<List<CellModel>> CellsMatrix
        {
            get => _cellsMatrix;

            protected set
            {
                if (_cellsMatrix == value) return;

                _cellsMatrix = value;
                RaisePropertyChanged();
            }
        }

        public BoardModel(int rows = 10, int columns = 10)
        {
            _rows = rows;
            _columns = columns;

            GenerateCells();
        }

        protected abstract void GenerateCells();
    }
}