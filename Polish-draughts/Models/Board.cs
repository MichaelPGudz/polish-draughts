using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Polish_draughts.Models
{
    public class Board
    {
        private static Board _instance;
        private readonly int _size;
        private readonly Pawn[,] Fields;

        private Board(int size)
        {
            this._size = size;
            Fields = new Pawn[_size, _size];
        }

        public static Board GetInstance(int size)
        {
            return _instance ??= new Board(size);
        }

        public int GetBoardSize()
        {
            var pole = _size * _size;
            return pole;
        }

        public Pawn[,] GetArray(int size)
        {
            return null;
        }

    }
}