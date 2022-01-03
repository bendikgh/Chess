using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Tile tile, Board board, char team): base(tile, board, team)
        {
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            List<Tile> lst = new List<Tile>();

            int m = this.Tile.M;
            int n = this.Tile.N;

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m + i, n + i);
                if (t == null) break;
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
                lst.Add(t);
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m - i, n - i);
                if (t == null) break;
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
                lst.Add(t);
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m + i, n - i);
                if (t == null) break;
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
                lst.Add(t);
            }

            for (int i = 1; i < 8; i++)
            {
                Tile t = this._board.getTileAtPos(m - i, n + i);
                if (t == null) break;
                else if (t.TilePiece != null)
                {
                    lst.Add(t);
                    break;
                }
                lst.Add(t);

            }

            return lst;
        }

        override
        public string ToString()
        {
            return base.ToString() + "B";
        }
    }
}
