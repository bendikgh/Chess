using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(Tile tile, Board board, char team) : base(tile, board, team)
        {
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            Tile t1 = this._board.getTileAtPos(this.Tile.M + 2, this.Tile.N + 1);
            Tile t2 = this._board.getTileAtPos(this.Tile.M + 2, this.Tile.N - 1);
            Tile t3 = this._board.getTileAtPos(this.Tile.M - 2, this.Tile.N + 1);
            Tile t4 = this._board.getTileAtPos(this.Tile.M - 2, this.Tile.N - 1);
            Tile t5 = this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N + 2);
            Tile t6 = this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N + 2);
            Tile t7 = this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N - 2);
            Tile t8 = this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N - 2);

            List<Tile> lst = new List<Tile>() { t1, t2, t3, t4, t5, t6, t7, t8 };

            return lst.FindAll(t => t != null);
        }

        public override string ToString()
        {
            return base.ToString() + "Kn";
        }
    }
}
