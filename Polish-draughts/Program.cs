using System;
using Polish_draughts.Models;
using Polish_draughts.Services;

namespace Polish_draughts
{
    class Program
    {
        // Get Board with proper amount and placement of Pawns
        private static Pawn[,] GetBoard()
        {
            Console.WriteLine("Enter Board size: ");
            
            var boardSize = Console.ReadLine();
            var result = Board.GetInstance(int.Parse(boardSize));
            var pole = result.GetBoardSize();
            var array = result.GetArray();
            int uBound0 = array.GetUpperBound(0); // Getting upper number of row
            int uBound1 = array.GetUpperBound(1); // Getting upper number of column 
            int amountOfBlackPawns = 0;
            int amountOfWhitePawns = 0;
            
            // Putting black pawns on board
            for (int i = 0; i <= uBound0; i++)
            {
                for (int j = 0; j <= uBound1; j++)
                {
                    // Amount of pawns for one player equals half of board size * half of board size minus 2 empty rows! 
                    if (amountOfBlackPawns < (int.Parse(boardSize) - 2) / 2 * (int.Parse(boardSize) / 2)) 
                    {
                        if (((i % 2 == 0) & (j % 2 == 1)) | ((i % 2 == 1) & (j % 2 == 0)))
                        {
                            var blackPawn = new Pawn(false);
                            blackPawn.Coordinates = (i, j);
                            array[i, j] = blackPawn;
                            amountOfBlackPawns += 1;
                        }
                    }
                }
            }
            
            // Putting white pawns on board
            for (int i = uBound0 / 2 + 2; i <= uBound0; i++) // Starting from one row after half of board (including two rows without pawns!)
            {
                for (int j = 0 ; j <= uBound1; j++)
                {
                    // Amount of pawns for one player equals half of board size * half of board size minus 2 empty rows! 
                    if (amountOfWhitePawns < (int.Parse(boardSize) - 2) / 2 * (int.Parse(boardSize) / 2)) 
                    {
                        if (((i % 2 == 0) & (j % 2 == 1)) | ((i % 2 == 1) & (j % 2 == 0)))
                        {
                            var whitePawn = new Pawn(true);
                            whitePawn.Coordinates = (i, j);
                            array[i, j] = whitePawn;
                            amountOfWhitePawns += 1;
                        }
                    }
                }
            }

            return array;
        }

        // Put Board on the screen with Console.WriteLine...
        private static void SeeBoard(Pawn[,] array)
        {
            int uBound0 = array.GetUpperBound(0);
            int uBound1 = array.GetUpperBound(1);
            for (int i = 0; i <= uBound0; i++)
            {
                for (int j = 0; j <= uBound1 ; j++)
                {
                    if (array[i, j] == null)
                        Console.Write("_");
                    else
                    {
                        Console.Write(array[i,j].Sign);
                    }
                }
                Console.WriteLine();
            }
        }
        
        // Main program below
        static void Main(string[] args)
        {
            
            Pawn[,] array = GetBoard();
            SeeBoard(array);
            var makingMove = new Game();
            Pawn[,] newArray = makingMove.MakeMove(array);
            Console.WriteLine();
            SeeBoard(newArray);

        }
    }
}