using Common.Base;
using System.Windows.Media;

namespace Common.Models
{
    public abstract class CellModel : ModelBase
    {
        public CellPosition Pos { get; protected set; }

        public abstract SolidColorBrush Brush { get; }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            protected set
            {
                if (_image == value) return;

                _image = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBack = true;
        public bool IsBack
        {
            get => _isBack;
            set
            {
                if (_isBack == value) return;

                _isBack = value;
                RaisePropertyChanged();
            }
        }

        public CellModel(CellPosition pos)
        {
            Pos = pos;
        }
    }
}