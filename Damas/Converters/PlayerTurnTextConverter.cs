using System;
using System.Windows.Data;

namespace Damas.Converters
{
    public class PlayerTurnTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (GameStates)(int)value;

            if (val == GameStates.WaitingMoveWhite || val == GameStates.WhiteSelected)
            {
                return "White player turn";
            }
            if (val == GameStates.WaitingMoveBlack || val == GameStates.BlackSelected)
            {
                return "Black player turn";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
