using System;
using System.Runtime.CompilerServices;
using Polish_draughts.Models;

namespace Polish_draughts.Services
{
    public class Game
    {
        public void newPlaceForPawn(int x, int y, int newX, int newY, Pawn[,] array)
        {
            var properPosition = isMoveValid(newX, newY, array);
            Console.WriteLine(properPosition);
            if (properPosition)
            {
                var pawn = array[x, y];
                array[x, y] = null;
                pawn.Coordinates = (newX, newY);
                array[newX, newY] = pawn;  
            }
            else
            {
                Console.WriteLine("Wrong coordinates - you are outside the board!");
            }

        }

        
        public bool isMoveValid(int newX, int newY, Pawn[,] array)
        {
            int uBound0 = array.GetUpperBound(0); // Getting upper number of row
            int uBound1 = array.GetUpperBound(1); // Getting upper number of column

            if (newX < uBound0 & newY < uBound1 & newX > 0 & newY > 0)
            {
                return true;
            }
            return false;
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
                    newPlaceForPawn(x, y, x - 1, y - 1, array);
                    break;
                
                case ConsoleKey.D9:
                    newPlaceForPawn(x, y, x - 1, y + 1, array);
                    break;
                
                case ConsoleKey.D1:
                    newPlaceForPawn(x, y, x + 1, y - 1, array);
                    break;
                
                case ConsoleKey.D3:
                    newPlaceForPawn(x, y,x + 1, y + 1, array);
                    break;
            }
            return array;
        }
    }
    
}