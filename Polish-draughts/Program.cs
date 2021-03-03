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
            var makingMove = new Game();
            int turn = 0;
            string message = "\nWhite's turn\n";
            string color = "W";
            bool nextMove;
            Console.Clear();
            Console.WriteLine(message);
            ShowBoard(array);

            while (true)
            {
                Pawn[,] previousMoveArray = (Pawn[,]) array.Clone();
                // making move gives also an information about next move -> is current player required to do next move
                // or is it allowed to change turn for other player
                nextMove = makingMove.MakeMove(color, array);
                bool boardEqual = AreBoardsEqual(array, previousMoveArray);
                // changing turns only if proper move was done (there is difference between current and previous array
                // and nextMove is not required - current player has no more pawns to beat
                if (!boardEqual && !nextMove)
                    turn += 1;
                message = (turn % 2 == 1) ? "Black's turn" : "White's turn";
                color = (turn % 2 == 1) ? "B" : "W";
                Console.WriteLine($"\n{message}\n");
                ShowBoard(array);
            }
        }

        private static int SetBoardSize()
        {
            int boardSize;
            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter Board size: ");
                    var userInput = Console.ReadLine();
                    boardSize = int.Parse(userInput);
                    break;
                }
                catch
                {
                    Console.WriteLine("\nWrong size!\n");
                }
            }
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("   ");
            for (int i = 0; i < (uBound0+1); i++)
            {
                Console.Write((char) ('A' + i));
                Console.Write(' ');
            }

            Console.WriteLine(' ');
            for (int i = 0; i <= uBound0; i++) 
            {
                if (i < 9) // number with one digit needs additional space to be right visual formatted in board
                {
                    Console.Write(' ');
                }

                Console.Write($"{i+1} ");
            for (int j = 0; j <= uBound1; j++)
                {
                    if (array[i, j] == null)
                        Console.Write("_ ");
                    else
                    {
                        if (array[i, j].Sign == "W")
                        {
                            Console.ForegroundColor = ConsoleColor.White;}
                        
                        if (array[i,j].Sign == "B")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        Console.Write($"{array[i, j].Sign} ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                }

                Console.WriteLine("");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Function checks if boards from current and previous moves are equal to see if there was 
        // done proper move -> needed for changing players
        private static bool AreBoardsEqual(Pawn[,] array1, Pawn[,] array2)
        {
            int uBound0 = array1.GetUpperBound(0);
            int uBound1 = array1.GetUpperBound(1);
            for (int i = 0; i <= uBound0; i++)
            {
                for (int j = 0; j <= uBound1; j++)
                {
                    if (array1[i, j] != array2[i, j])
                        return false;
                }
            }
            return true;
        }
    }
}