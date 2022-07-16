using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MineSweeper
{
    public static class Globals
    {
        public static ImageSource CellBack =
            new BitmapImage(new Uri("/Assets/Images/CardBack.jpg", UriKind.Relative));

        public static ImageSource MineImg =
                    new BitmapImage(new Uri("/Assets/Images/mine.png", UriKind.Relative));

        public static int Rows = 10;
        public static int Columns = 10;
        public static int Mines = 20;
    }
}