using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class King: Piece
    {
        public King(Tile tile, Board board, char team) : base(tile, board, team)
        {
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            List<Tile> lst = new List<Tile>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    lst.Add(this._board.getTileAtPos(this.Tile.M + i, this.Tile.N + j));
                }
            }

            return lst.FindAll(t => t != null &&
            this._board.getAllTilesUnderAttackBy(this.Team == 'W' ? 'B' : 'W').Contains(t) == false);
        }

        // 
        override
        public string ToString()
        {
            return base.ToString() + "K";
        }


    }
}
