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
                }
            }

            for (var i = 1; i <= boardSize; i++)
            {
                if (i == coordinates[1])
                {
                    transformedCoordinates[1] = coordinates[1];
                }
            }

            return transformedCoordinates;
        }
        
        
        private static bool IsPositionInsideBoard(int newX, int newY, Pawn[,] array)
        {
            int uBound0 = array.GetUpperBound(0); // Getting upper number of row
            int uBound1 = array.GetUpperBound(1); // Getting upper number of column

            if (newX <= uBound0 & newY <= uBound1 & newX >= 0 & newY >= 0)
            {
                return true;
            }
            Console.WriteLine();
            Console.WriteLine("Your new position is outside board");
            return false;
        }
        

        // Method below checks if there is free field on next index in array
        private static bool IsNextFieldFree(int newX, int newY, Pawn[,] array)
        {
            var properPosition = IsPositionInsideBoard(newX, newY, array);
            if (properPosition)
            {
                if (array[newX, newY] == null)
                {
                    return true;
                }
                Console.WriteLine("Your destination is taken");
                return false;
            }
            return false;
        }

        // Method below checks if there is free field for pawn behind the taken field
        // (two indexes in array further) with pawn of enemy
        private static bool IsFieldBehindPawnFree(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var directionX = newX - x;
            var directionY = newY - y;
            newX += directionX;
            newY += directionY;

            var properPosition = IsPositionInsideBoard(newX, newY, array);
            if (properPosition)
            {
                if (array[newX, newY] == null)
                {
                    return true;
                }
                Console.WriteLine("Your destination is taken");
                return false;
            }
            return false;
        }


        private static bool? IsMoveValid(string color, int x, int y, int newX, int newY, Pawn[,] array)
        {
            // If field has pawn
            if (array[x, y] != null)
            {   
                //  If the pawn has colour of current player
                if (array[x, y].Sign == color)
                {
                    // RETURN true if there is place on next field so you can jump on next field
                    if (IsNextFieldFree(newX, newY, array)) 
                        return true;    
                    Console.Clear();
                    // RETURN null if there is no place on next field, but is two fields further AND
                    // there is your enemy's pawn between - jump two fields
                    if (IsFieldBehindPawnFree(x, y, newX, newY, array))
                        if (array[newX,newY].Sign != color)
                            return null;
                        else
                        {
                            Console.WriteLine("\nYou are trying to beat your own pawn!"); 
                        }
                    return false;
                }
                else
                {
                    Console.WriteLine("\nYou are trying to move not your pawn!");
                }
            }
            else
            {
                Console.WriteLine("\nYou've chosen field without pawn");
            }
            return false;
        }
        

        private static bool IsNextMoveRequired(string color, int newX, int newY, Pawn[,] array)
        {
            if (IsFieldBehindPawnFree(newX, newY, newX + 1, newY + 1, array))
            {
                if (array[newX + 1, newY + 1] != null)
                {
                    if (array[newX + 1, newY + 1].Sign != color)
                    {
                        Console.Clear();
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX - 1, newY - 1, array))
            {
                if (array[newX - 1, newY - 1] != null)
                {
                    if (array[newX - 1, newY - 1].Sign != color)
                    {
                        Console.Clear();
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX + 1, newY - 1, array))
            {
                if (array[newX + 1, newY - 1] != null)
                {
                    if (array[newX + 1, newY - 1].Sign != color)
                    {
                        Console.Clear();
                        return true;
                    }
                } 
            }
            if (IsFieldBehindPawnFree(newX, newY, newX - 1, newY + 1, array))
            {
                if (array[newX - 1, newY + 1] != null)
                {
                    if (array[newX - 1, newY + 1].Sign != color)
                    {
                        Console.Clear();
                        return true;
                    }
                } 
            }
            Console.Clear();
            return false;
        }
        

        private static bool NewPlaceForPawn(string color, int x, int y, int newX, int newY, Pawn[,] array)
        {
            var validOneMove = IsMoveValid(color, x, y, newX, newY, array);
            if (validOneMove == null) // there is possibility to take enemy's pawn and jump for two fields
            {
                array[newX, newY] = null; // take pawn of your enemy - make its field "null"
                var directionX = newX - x; // calculate direction of your move in array
                var directionY = newY - y;
                // overwrite your destination coordinates to allow you jump by two fields
                newX += directionX; 
                newY += directionY;
            }
            Console.WriteLine();
            if (validOneMove == true | validOneMove == null)
            {
                // for true you move by one field, for null you move by two with
                // deleting enemy's pawn from board
                var pawn = array[x, y];
                array[x, y] = null;
                pawn.Coordinates = (newX, newY);
                array[newX, newY] = pawn;
                var nextMove = IsNextMoveRequired(color, newX, newY, array);
                if (nextMove)
                    return true;
            }
            else
            {
                Console.WriteLine("Wrong move!");
            }
            return false;
        }
        
        
        public bool MakeMove(string color, Pawn[,] array)
        {   
            Console.WriteLine("Please provide coordinates of Pawn you want to move: eg. 13, or 41");
            var coordinates = Console.ReadLine();
            // var transformedCoordinates = TransformCoordinates(10, coordinates);
            // Console.WriteLine(transformedCoordinates);
            char[] coordinatesSplitted = coordinates.ToCharArray();
            int x = Int32.Parse(coordinatesSplitted[0].ToString());
            int y = Int32.Parse(coordinatesSplitted[1].ToString());

            Console.WriteLine(
                "Where do you want to move pawn? D7 for left up, D9 for right up, D1 for left down, D3 for right down");
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D7:
                    Console.Clear();
                    if (NewPlaceForPawn(color, x, y, x - 1, y - 1, array))
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
                    Console.WriteLine("Wrong button!");
                    break;
            }
            return false;
        }
    }
}