using System;
using System.Runtime.CompilerServices;
using Polish_draughts.Models;

namespace Polish_draughts.Services
{
    public class Game
    {
        private bool IsPositionInsideBoard(int newX, int newY, Pawn[,] array)
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

        private bool IsNextFieldFree(int newX, int newY, Pawn[,] array)
        {
            if (array[newX, newY] == null)
            {
                return true;
            }
            return false;
        }


        private bool IsMoveValid(int x, int y, int newX, int newY, Pawn[,] array)
        {
            if (array[x, y] != null)
            {
                var properPosition = IsPositionInsideBoard(newX, newY, array);
                if (properPosition)
                {
                    if (IsNextFieldFree(newX, newY, array))
                        return true;
                    Console.WriteLine();
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

        private void NewPlaceForPawn(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var validMove = IsMoveValid(x, y, newX, newY, array);
            Console.WriteLine();
            if (validMove)
            {
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
            }
            return array;
        }
    }
    
}