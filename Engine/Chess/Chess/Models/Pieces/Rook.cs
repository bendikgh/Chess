using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook(Tile tile, Board board, char team) : base(tile, board, team)
        {
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            List<Tile> lst = new List<Tile>();
            int m = this.Tile.M;
            int n = this.Tile.N;

            //this.TilesUnderAttack = new List<Tile>();

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m + i, n);
                if (t == null) break;
                if (t.TilePiece == null) lst.Add(t);
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m - i, n);
                if (t == null) break;
                if (t.TilePiece == null) lst.Add(t);
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m, n - i);
                if (t == null) break;
                if (t.TilePiece == null) lst.Add(t);
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m, n + i);
                if (t == null) break;
                if (t.TilePiece == null) lst.Add(t);
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
            }

            return lst;
        }

        override
        public string ToString()
        {
            return base.ToString() + "R";
        }
    }
}
