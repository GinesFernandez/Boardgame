using Common.Base;
using System.Windows.Media;

namespace Common.Models
{
    public abstract class CellModel : ModelBase
    {
        public CellPosition Pos { get; protected set; }

        public virtual SolidColorBrush Brush { get; set; } = new SolidColorBrush(Colors.Black);

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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;

                _isSelected = value;
                RaisePropertyChanged();

                IsBack = !_isSelected;
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