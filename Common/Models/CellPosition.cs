
namespace Common.Models
{
    public class CellPosition
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public CellPosition(int posx, int posy)
        {
            PosX = posx;
            PosY = posy;
        }
    }
}
