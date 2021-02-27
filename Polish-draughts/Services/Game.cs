using System;
using System.Runtime.CompilerServices;
using Polish_draughts.Models;

namespace Polish_draughts.Services
{
    public class Game
    {
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
            if (array[newX, newY] == null)
            {
                return true;
            }
            return false;
        }

        // Method below checks if there is free field for pawn behind the taken field
        // (two indexes in array further) with pawn of enemy
        private static bool IsFieldBehindPawnFree(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var directionX = newX - x;
            var directionY = newY - y;

            if (array[newX + directionX, newY + directionY] == null)
            {
                return true;
            }
            return false;
        }


        private static bool? IsMoveValid(int x, int y, int newX, int newY, Pawn[,] array)
        {
            if (array[x, y] != null)
            {
                var properPosition = IsPositionInsideBoard(newX, newY, array);
                if (properPosition)
                {
                    if (IsNextFieldFree(newX, newY, array)) // RETURN true if there is place on next field
                        return true;                        // so you can jump on next field
                    if (IsFieldBehindPawnFree(x, y, newX, newY, array)) // RETURN null if there is no place
                        return null;                                    // on next field, but is two fields
                    Console.WriteLine();                                // further - jump two fields
                    Console.WriteLine("Your destination is taken");
                    return false;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You've chosen field without proper pawn");
            }
            return false;
        }

        private static void NewPlaceForPawn(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var validOneMove = IsMoveValid(x, y, newX, newY, array);
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
            }
            else
            {
                Console.WriteLine("Wrong move!");
            }
        }

        
        public Pawn[,] MakeMove(Pawn[,] array)
        {   
            Console.WriteLine("Please provide coordinates of Pawn you want to move: eg. 13, or 41");
            var coordinates = Console.ReadLine();
            char[] coordinatesSplitted = coordinates.ToCharArray();
            int x = Int32.Parse(coordinatesSplitted[0].ToString());
            int y = Int32.Parse(coordinatesSplitted[1].ToString());
            Console.WriteLine(coordinatesSplitted[0]);
            Console.WriteLine(coordinatesSplitted[1]);
            //var pawnColour = array[x, y].Sign;
            
            Console.WriteLine(
                "Where you want to move pawn? D7 for left up, D9 for right up, D1 for left down, D3 for right down");
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D7:
                    NewPlaceForPawn(x, y, x - 1, y - 1, array);
                    break;
                
                case ConsoleKey.D9:
                    NewPlaceForPawn(x, y, x - 1, y + 1, array);
                    break;
                
                case ConsoleKey.D1:
                    NewPlaceForPawn(x, y, x + 1, y - 1, array);
                    break;
                
                case ConsoleKey.D3:
                    NewPlaceForPawn(x, y,x + 1, y + 1, array);
                    break;
                
                default:
                    Console.WriteLine("Wrong button!");
                    break;
            }
            return array;
        }
    }
    
}