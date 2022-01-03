using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class Pawn: Piece
    {

        public Pawn(Tile tile, Board board, char team) : base(tile, board, team)
        {
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            List<Tile> lst = new List<Tile>();

            if (this.Team == 'W')
            {
                Tile t1 = this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N);

                if (t1 != null && t1.TilePiece == null) lst.Add(t1);

                if (this.Tile.M == 1)
                {
                    Tile t2 = this._board.getTileAtPos(this.Tile.M + 2, this.Tile.N);
                    if (t2 != null && t2.TilePiece == null) lst.Add(t2);
                }

                if (this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N + 1) != null &&
                    this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N + 1).TilePiece != null)
                {
                    lst.Add(this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N + 1));
                }


                if (this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N - 1) != null &&
                    this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N - 1).TilePiece != null)
                {
                    lst.Add(this._board.getTileAtPos(this.Tile.M + 1, this.Tile.N - 1));
                }

                return lst;
            }
            else {
                Tile t1 = this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N);

                if (t1 != null && t1.TilePiece == null) lst.Add(t1);

                if (this.Tile.M == 1)
                {
                    Tile t2 = this._board.getTileAtPos(this.Tile.M - 2, this.Tile.N);
                    if (t2 != null && t2.TilePiece == null) lst.Add(t2);
                }

                if (this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N + 1) != null &&
                    this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N + 1).TilePiece != null)
                {
                    lst.Add(this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N + 1));
                }


                if (this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N - 1) != null &&
                    this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N - 1).TilePiece != null)
                {
                    lst.Add(this._board.getTileAtPos(this.Tile.M - 1, this.Tile.N - 1));
                }

                return lst;
            }
        }

        override
        public string ToString()
        {
            return base.ToString() + "P";
        }
    }
}
