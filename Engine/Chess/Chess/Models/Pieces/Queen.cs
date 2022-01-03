using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(Tile tile, Board board, char team) : base(tile, board, team)
        {
            this.Bishop = new Bishop(tile, board, team);
            this.Rook = new Rook(tile, board, team);
            this.Bishop.setLegalTile(tile);
            this.Rook.setLegalTile(tile);
        }

        public Piece Bishop { get; init; }
        public Piece Rook { get; init; }

        override
        internal void setTile(Tile tile)
        {
            this._tile = tile;
            this._tile.TilePiece = this;
            this.Rook.setLegalTile(tile);
            this.Bishop.setLegalTile(tile);
        }

        override
        internal void setLegalTile(Tile tile)
        {
            this._tile = tile;
            this._tile.TilePiece = this;
            this.Rook.setLegalTile(tile);
            this.Bishop.setLegalTile(tile);
        }


        override
        public string ToString()
        {
            return base.ToString() + "Q";
        }

        public override List<Tile> getAllTilesUnderAttack()
        {
            Tile t = this.Tile;
            // Legal moves
            List<Tile> tmp1 = this.Bishop.getAllTilesUnderAttack();
            List<Tile> tmp2 = this.Rook.getAllTilesUnderAttack();

            List<Tile> lst = new List<Tile>(tmp1);
            lst.AddRange(tmp2);
            return lst;
        }
    }
}
