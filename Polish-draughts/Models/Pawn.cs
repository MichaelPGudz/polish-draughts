using System;
namespace Polish_draughts.Models
{
    public class Pawn
    {
        public bool IsWhite{get; set;}
        
        public (int x, int y) Coordinates{get; set;}
        
        public bool IsCrowned{ get; set; }

        public string Sign { get; set; } // Sign visualises Pawn on board

        public Pawn(bool pawnColor)
        {
            IsWhite = pawnColor; // True for White color, False for Black color
            Sign = IsWhite ? "W" : "B"; // If color is White let Sign be "W", if Black, then "B"
        }
    }
    
    
    
}