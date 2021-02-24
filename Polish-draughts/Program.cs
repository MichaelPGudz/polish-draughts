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
                            var blackPawn = new Pawn(true);
                            blackPawn.Coordinates = (i, j);
                            array[i, j] = blackPawn;
                            amountOfWhitePawns += 1;
                        }
                    }
                }
            }

             void SeeBoard()
            {
                for (int i = 0; i <= uBound0; i++)
                {
                    for (int j = 0; j <= uBound1; j++)
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

            SeeBoard();

        }
    }
}