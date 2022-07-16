using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorMemoryGame
{
    public static class Globals
    {
        public static ImageSource CellBack =
            new BitmapImage(new Uri("/Assets/Images/CardBack.png", UriKind.Relative));

        public static int Rows = 4;
        public static int Columns = 4;
    }
}