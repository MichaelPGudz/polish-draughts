using System;
using Polish_draughts.Models;
using Polish_draughts.Services;

namespace Polish_draughts
{
    class Program
    {
        static void Main(string[] args)
        {
            Pawn[,] array = GetBoard();
            ShowBoard(array);
            var makingMove = new Game();
            while (true)
            {
                Pawn[,] newArray = makingMove.MakeMove(array);
                Console.WriteLine();
                ShowBoard(newArray);
            }

        }

        private static int SetBoardSize()
        {
            Console.WriteLine("Enter Board size: ");
            var userInput = Console.ReadLine();
            var boardSize = int.Parse(userInput);
            return boardSize;
        }
        // Get Board with proper amount and placement of Pawns
        private static Pawn[,] GetBoard()
        {
            var boardSize = SetBoardSize();
            var result = Board.GetInstance(boardSize);
            var pole = result.GetBoardSize();
            var array = result.GetArray();

            void PopulateBoard(Pawn[,] array, int boardSize, bool isWhite)
            {
                int pawnsAmount = 0;
                int uBound0 = array.GetUpperBound(0); // Getting upper number of row
                int uBound1 = array.GetUpperBound(1); // Getting upper number of column 
                int iterator = !isWhite ? 0 : uBound0 / 2 + 2;
                for (int i = iterator; i <= uBound0; i++)
                {
                    for (int j = 0; j <= uBound1; j++)
                    {
                        // Amount of pawns for one player equals half of board size * half of board size minus 2 empty rows! 
                        if (pawnsAmount < (boardSize - 2) / 2 * (boardSize / 2))
                        {
                            if (((i % 2 == 0) & (j % 2 == 1)) | ((i % 2 == 1) & (j % 2 == 0)))
                            {
                                var pawnColor = new Pawn(isWhite);
                                pawnColor.Coordinates = (i, j);
                                array[i, j] = pawnColor;
                                pawnsAmount += 1;
                            }
                        }
                    }
                }
            }

            // Putting black pawns on board
            PopulateBoard(array, boardSize, false);
            // // Putting white pawns on board
            PopulateBoard(array, boardSize, true);
            return array;
        }

        // Put Board on the screen with Console.WriteLine...
        private static void ShowBoard(Pawn[,] array)
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
        
    }
}