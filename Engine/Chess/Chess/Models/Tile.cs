using System;
using Chess.Models.Pieces;

namespace Chess.Models
{
    public class Tile
    {
        public Tile(int x, int y)
        {
            this.M = x;
            this.N = y;
        }

        private Piece _tilePiece;

        public int M { get; init; }
        public int N { get; init; }
        public Piece TilePiece { get {
                return this._tilePiece;
            } set {
                this._tilePiece = value;                
            } }

        override
        public string ToString()
        {
            if (this.TilePiece != null)
            {
                return TilePiece.ToString();
            }
            return "";
        }
    }
}
