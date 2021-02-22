using System.Drawing;

namespace Polish_draughts.Models
{
    public class Board
    {
        private static Board _instance;
        private int _size;

        private Board(int size)
        {
            this._size = size;
        }

        public static Board GetInstance(int size)
        {
            return _instance ??= new Board(size);
        }
    }
}