using System;
namespace Chess.Models
{
    public class Move
    {
        public Move(Piece brick, Tile current, Tile destionation)
        {
            this.Brick = brick;
            this.CurrentTile = current;
            this.DestionationTile = destionation;
        }

        public Piece Brick { get; init; }

        public Tile CurrentTile { get; init; }

        public Tile DestionationTile { get; init; }

    }
}
