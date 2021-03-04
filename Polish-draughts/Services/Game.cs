using System;
using Polish_draughts.Models;

namespace Polish_draughts.Services
{
    public class Game
    {
        public int[] TransformCoordinates(int boardSize, string coordinates)
        {
            char[] characters = new char[boardSize];
            int [] transformedCoordinates = new int [2];
            for (int i = 0; i < boardSize; i++)
            {
                characters[i] = (char)('A' + i);
            }

            for (var i = 0; i < characters.Length; i++)
            {
                if (characters[i] == coordinates[0])
                {
                    transformedCoordinates[0] = i;
                    break;
                }
            }

            for (var i = 1; i <= boardSize; i++)
            {
                if (i == Int32.Parse(coordinates.Substring(1)))
                {
                    transformedCoordinates[1] = i - 1;
                    break;
                }
            }

            return transformedCoordinates;
        }
        
        
        private Tuple<bool, string> IsPositionInsideBoard(int newX, int newY, Pawn[,] array)
        {
            int uBound0 = array.GetUpperBound(0); // Getting upper number of row
            int uBound1 = array.GetUpperBound(1); // Getting upper number of column

            if (newX <= uBound0 & newY <= uBound1 & newX >= 0 & newY >= 0)
            {
                return new Tuple<bool, string>(true, String.Empty);
            }
            string message = "Your new position is outside board";
            return new Tuple<bool, string>(false, message);
        }
        

        // Method below checks if there is free field on next index in array
        private Tuple<bool, string> IsNextFieldFree(int newX, int newY, Pawn[,] array)
        {
            var properPosition = IsPositionInsideBoard(newX, newY, array);
            if (properPosition.Item1)
            {
                if (array[newX, newY] == null)
                {
                    return new Tuple<bool, string>(true, String.Empty);
                }
                string message = "Your destination is taken";
                return new Tuple<bool, string>(false, message);
            }
            return new Tuple<bool, string>(false, properPosition.Item2);
        }

        // Method below checks if there is free field for pawn behind the taken field
        // (two indexes in array further) with pawn of enemy
        private Tuple<bool, string> IsFieldBehindPawnFree(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var directionX = newX - x;
            var directionY = newY - y;
            newX += directionX;
            newY += directionY;

            var properPosition = IsPositionInsideBoard(newX, newY, array);
            if (properPosition.Item1)
            {
                if (array[newX, newY] == null)
                {
                    return new Tuple<bool, string>(true, String.Empty);
                }
                string message = "Your destination is taken";
                return new Tuple<bool, string>(false, message);
            }
            return new Tuple<bool, string>(false, properPosition.Item2);
        }


        private Tuple<bool?, string> IsMoveValid(string color, int x, int y, int newX, int newY, Pawn[,] array)
        {
            string message = "";
            // If field has pawn
            if (array[x, y] != null)
            {   
                //  If the pawn has colour of current player
                if (array[x, y].Sign == color)
                {
                    // RETURN true if there is place on next field so you can jump on next field
                    var nextFieldFree = IsNextFieldFree(newX, newY, array);
                    if (nextFieldFree.Item1) 
                        return new Tuple<bool?, string>(true, String.Empty);
                    // RETURN null if there is no place on next field, but is two fields further AND
                    // there is your enemy's pawn between - jump two fields
                    var fieldBehindFree = IsFieldBehindPawnFree(x, y, newX, newY, array);
                    if (fieldBehindFree.Item1)
                        if (array[newX,newY].Sign != color)
                            return new Tuple<bool?, string>(null, String.Empty);
                        else
                        {
                            message = "You are trying to beat your own pawn";
                            return new Tuple<bool?, string>(false, message);
                        }

                    message = fieldBehindFree.Item2;
                    return new Tuple<bool?, string>(false, message);
                }
                else
                {
                    message = "You are trying to move not your pawn!";
                    return new Tuple<bool?, string>(false, message);
                }
            }
            else
            {
                message = "You've chosen field without pawn";
                return new Tuple<bool?, string>(false, message);
            }
        }
        
        
        // function checks if there are other enemy's pawns around newX and newY destination
        // if yes, then checks if there are free fields within board boundaries behind
        // those pawns -> returns true (next move of the same player required)
        // if no, or there is no free fields -> returns false (next move is not required)
        private bool IsNextMoveRequired(string color, int newX, int newY, Pawn[,] array)
        {
            if (IsFieldBehindPawnFree(newX, newY, newX + 1, newY + 1, array).Item1)
            {
                if (array[newX + 1, newY + 1] != null)
                {
                    if (array[newX + 1, newY + 1].Sign != color)
                    {
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX - 1, newY - 1, array).Item1)
            {
                if (array[newX - 1, newY - 1] != null)
                {
                    if (array[newX - 1, newY - 1].Sign != color)
                    {
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX + 1, newY - 1, array).Item1)
            {
                if (array[newX + 1, newY - 1] != null)
                {
                    if (array[newX + 1, newY - 1].Sign != color)
                    {
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX - 1, newY + 1, array).Item1)
            {
                if (array[newX - 1, newY + 1] != null)
                {
                    if (array[newX - 1, newY + 1].Sign != color)
                    {
                        return true;
                    }
                } 
            }
            return false;
        }
        

        private bool NewPlaceForPawn(string color, int x, int y, int newX, int newY, Pawn[,] array)
        {
            var validOneMove = IsMoveValid(color, x, y, newX, newY, array);
            if (validOneMove.Item1 == null) // there is possibility to take enemy's pawn and jump for two fields
            {
                array[newX, newY] = null; // take pawn of your enemy - make its field "null"
                var directionX = newX - x; // calculate direction of your move in array
                var directionY = newY - y;
                // overwrite your destination coordinates to allow you jump by two fields
                newX += directionX; 
                newY += directionY;
            }
            Console.WriteLine();
            if (validOneMove.Item1 == true | validOneMove.Item1 == null)
            {
                // for true you move by one field, for null you move by two with
                // deleting enemy's pawn from board
                var pawn = array[x, y];
                array[x, y] = null;
                pawn.Coordinates = (newX, newY);
                array[newX, newY] = pawn;
                // checks while moving to new coordinates after beating if there is required next move of the same player
                // If move is required -> returns true
                // If not -> function goes further and, finally, returns false
                if (validOneMove.Item1 == null)
                {
                    var nextMove = IsNextMoveRequired(color, newX, newY, array);
                    if (nextMove)
                        return true;
                }
            }
            else
            {
                Console.WriteLine(validOneMove.Item2);
            }
            return false;
        }
        
        
        public bool MakeMove(string color, Pawn[,] array)
        {
            int x;
            int y;
            Console.WriteLine("\nPlease provide coordinates of Pawn you want to move: eg. A1, or H7");
            while (true)
            {
                var coordinates = Console.ReadLine();
                try
                {
                    var transformedCoordinates = TransformCoordinates(array.GetUpperBound(0) + 1, coordinates);
                    x = transformedCoordinates[1];
                    y = transformedCoordinates[0];
                    break;
                }
                catch
                {
                    Console.WriteLine("\nWrong coordinates!\n");
                }
            }


            Console.WriteLine(
                "\nWhere do you want to move pawn? D7 for left up, D9 for right up, D1 for left down, D3 for right down");
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D7:
                    Console.Clear();
                    if (NewPlaceForPawn(color, x, y, x - 1, y - 1, array))
                        // returns true after move in all cases, when NewPlaceForPawn
                        // is true -> next move of current player is required
                        return true;
                    break;
                
                case ConsoleKey.D9:
                    Console.Clear();
                    if (NewPlaceForPawn(color, x, y, x - 1, y + 1, array))
                        return true;
                    break;
                
                case ConsoleKey.D1:
                    Console.Clear();
                    if (NewPlaceForPawn(color, x, y, x + 1, y - 1, array))
                        return true;
                    break;
                
                case ConsoleKey.D3:
                    Console.Clear();
                    if (NewPlaceForPawn(color, x, y, x + 1, y + 1, array))
                        return true;
                    break;
                
                default:
                    Console.WriteLine("\nWrong button!\n");
                    break;
            }
            // returns false after move when there is no need of current's player next move
            return false;
        }


        public Tuple<bool, string> IsWinnerByBeat(Pawn[,] array)
        {
            int blackPawns = 0;
            int whitePawns = 0;
            
            for (int i = 0; i <= array.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= array.GetUpperBound(1); j++)
                {
                    if (array[i,j] != null)
                    {
                        if (array[i, j].Sign == "W")
                            whitePawns += 1;
                        if (array[i, j].Sign == "B")
                            blackPawns += 1;
                    }
                }
            }
            if (whitePawns == 0)
            {
                return new Tuple<bool, string>(true, "Black has won!");
            }
            if (blackPawns == 0)
            {
                return new Tuple<bool, string>(true, "White has won!");
            }
            return new Tuple<bool, string>(false, String.Empty);
        }
    }
}