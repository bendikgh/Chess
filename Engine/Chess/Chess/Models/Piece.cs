    using System;
using System.Collections.Generic;
using Chess.Models.Pieces;

namespace Chess.Models
{
    public abstract class Piece
    {
        protected Tile _tile;   
        protected Board _board;

        public Tile Tile { get {
                return this._tile;
            } set {
                if (this.GetLegalTiles().Contains(value)) {
                    this.setTile(value);
                    return;
                }
                throw new ArgumentException("The brick cannot take this position");
            } }

        internal virtual void setTile(Tile tile)
        {
            this._tile = tile;
            this._tile.TilePiece = this;
        }

        internal virtual void setLegalTile(Tile tile)
        {
            this._tile = tile;
            this._tile.TilePiece = this;
        }


        public Piece(Tile tile, Board board, char team)
        {
            this._tile = tile;
            this._board = board;
            this.Team = team;
            this._tile.TilePiece = this;
        }

        public List<Tile> GetLegalTiles()
        {
            if (this._board.checkTeamInChess(this.Team))
            {
                List<Tile> legalTiles = new List<Tile>();
                List<Tile> lst = this.getAllTilesUnderAttack().FindAll(t => t.TilePiece is not King);
                foreach (Tile tile in lst)
                {
                    Move move = new Move(this, this.Tile, tile);
                    Board newBoard = this._board.generateSuccessor(move);
                    if (newBoard.checkTeamInChess(this.Team) == false) legalTiles.Add(tile);
                }
                return legalTiles.FindAll(t => t.TilePiece == null || (t.TilePiece.Team != Team && t.TilePiece is not King));
            }
            return this.getAllTilesUnderAttack().FindAll(t => t.TilePiece == null || (t.TilePiece.Team != Team && t.TilePiece is not King));
        }

        public char Team { get; set; }

        public static Piece makeBrick(string brick, Tile tile, Board board)
        {
            // Find the corresponding brick
            if (brick == "" || brick == null)
            {
                return null;
            }
            char team = brick[0];
            string b = brick.Substring(1);
            switch (b)
            {
                case "R":
                    return new Rook(tile, board, team);
                case "Kn":
                    return new Knight(tile, board, team);
                case "B":
                    return new Bishop(tile, board, team);
                case "P":
                    return new Pawn(tile, board, team);
                case "K":
                    return new King(tile, board, team);
                case "Q":
                    return new Queen(tile, board, team);
                default:
                    return null;
            }
        }

        override
        public string ToString()
        {
            return this.Team.ToString();
        }

        public abstract List<Tile> getAllTilesUnderAttack();

    }       
}
