using System;
using System.Runtime.CompilerServices;
using Polish_draughts.Models;

namespace Polish_draughts.Services
{
    public class Game
    {
        public void newPlaceForPawn(string pawnColour, int x, int y, int newX, int newY, Pawn[,] array)
        {
            array[x, y] = null;

            if (pawnColour == "B")
            {
                var blackPawn = new Pawn(false);
                blackPawn.Coordinates = (newX, newY);
                array[newX, newY] = blackPawn; 
            }
            else if (pawnColour == "W")
            {
                var whitePawn = new Pawn(true);
                whitePawn.Coordinates = (newX, newY);
                array[newX, newY] = whitePawn;
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
            var pawnColour = array[x, y].Sign;
            
            Console.WriteLine(
                "Where you want to move pawn? D7 for left up, D9 for right up, D1 for left down, D3 for right down");
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D7:
                    newPlaceForPawn(pawnColour, x, y, x - 1, y - 1, array);
                    break;
                
                case ConsoleKey.D9:
                    newPlaceForPawn(pawnColour, x, y, x - 1, y + 1, array);
                    break;
                
                case ConsoleKey.D1:
                    newPlaceForPawn(pawnColour, x, y, x + 1, y - 1, array);
                    break;
                
                case ConsoleKey.D3:
                    newPlaceForPawn(pawnColour, x, y,x + 1, y - 1, array);
                    break;
            }
            return array;
        }
    }
    
}