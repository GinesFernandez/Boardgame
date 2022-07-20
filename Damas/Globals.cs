using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Damas
{
    public static class Globals
    {
        public static int Rows = 8;
        public static int Columns = 8;

        public static ImageSource QueenImg =
                    new BitmapImage(new Uri("/Assets/Images/logo.png", UriKind.Relative));
    }
}