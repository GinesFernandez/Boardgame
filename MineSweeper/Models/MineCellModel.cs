using System.Windows.Media;
using Common.Models;

namespace MineSweeper.Models
{
    public class MineCellModel : CellModel
    {
        private int? _value;
        public int? Value
        {
            get => _value;
            protected set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;
                RaisePropertyChanged();
                RaisePropertyChanged(Text);
            }
        }

        public string Text => _value == null ? "*" : _value.ToString();

        public override SolidColorBrush Brush => Value == 0 ?
                                            new SolidColorBrush(Colors.Lime)
                                            : (Value == 1 ?
                                                new SolidColorBrush(Colors.Blue)
                                                : (Value == 2 ?
                                                    new SolidColorBrush(Colors.Green)
                                                    : (Value == 3 ?
                                                        new SolidColorBrush(Colors.Red)
                                                        : (Value == 4 ?
                                                            new SolidColorBrush(Colors.Purple)
                                                            : new SolidColorBrush(Colors.Black)))));

        private ImageSource _imageMine = Globals.MineImg;
        public ImageSource ImageMine
        {
            get => _imageMine;
            set
            {
                if (_imageMine == value) return;

                _imageMine = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMine => Value == null;

        public MineCellModel(CellPosition pos, int? value) : base(pos)
        {
            Value = value;
            Image = Globals.CellBack;
        }
    }
}