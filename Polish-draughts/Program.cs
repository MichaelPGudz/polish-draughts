using System;
using Polish_draughts.Models;

namespace Polish_draughts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Board size: ");
            
            var boardSize = Console.ReadLine();
            var result = Board.GetInstance(int.Parse(boardSize));
            var pole = result.GetBoardSize();
            Console.WriteLine(pole);
            Console.WriteLine($"Your board size: {boardSize} and {result}");

            var whitePawn = new Pawn(false);
            Console.WriteLine(whitePawn.Sign);

        }
    }
}