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
            Console.WriteLine($"Your board size: {boardSize} and {result}");

        }
    }
}