using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Damas.Converters
{
    public class ValueToBrushConverter : IValueConverter
    {
        private static readonly Brush __brushWhite = new SolidColorBrush(Colors.WhiteSmoke);
        private static readonly Brush __brushBlack = new SolidColorBrush(Colors.DarkRed);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as CellState?;

            if (val == CellState.WhiteToken || val == CellState.WhiteQueen)
            {
                return __brushWhite;
            }
            if (val == CellState.BlackToken || val == CellState.BlackQueen)
            {
                return __brushBlack;
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
