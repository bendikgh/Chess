using System;
using System.Collections.Generic;
using System.Text.Json;
using Chess.Models.Pieces;

namespace Chess.Models
{
    public class Board
    {

        private List<List<Tile>> _chessBoard;

        private List<List<Tile>> ChessBoard {
            get {
                return this._chessBoard;
            }
            set {
                this._chessBoard = value;
            } }

        public Board(Board board)
        {
            List<List<Tile>> lst = new List<List<Tile>>();
            foreach (List<Tile> l in board._chessBoard)
            {
                List<Tile> tmp = new List<Tile>();
                foreach (Tile t in l)
                {
                    Tile tmpTile = new Tile(t.M, t.N);

                    string s = t.TilePiece != null ? t.TilePiece.ToString() : null;

                    // Uses Brick.makebrick function
                    tmpTile.TilePiece = t.TilePiece != null ? Piece.makeBrick(t.TilePiece.ToString(), tmpTile, this) : null;
                    tmp.Add(tmpTile);
                }
                lst.Add(tmp);
            }
            this._chessBoard = lst;
        }

        public Board(string board)
        {
            this.makeBoard(board);
        }

        private void makeBoard(string boardString)
        {
            List<List<string>> board = JsonSerializer.Deserialize<List<List<string>>>(boardString);

            // Make a board with bricks
            List<List<Tile>> lst = new List<List<Tile>>();

            for (int i = 0; i < 8; i++)
            {
                List<Tile> tmp = new List<Tile>();
                for (int j = 0; j < 8; j++)
                {
                    Tile t = new Tile(i, j);
                    tmp.Add(t);
                    t.TilePiece = Piece.makeBrick(board[i][j], t, this);
                }
                lst.Add(tmp);
            }

            this.ChessBoard = lst;
        }

        override
        public string ToString()
        {
            string tmp = "[";
            foreach(List<Tile> lst in this.ChessBoard)
            {
                string s = "[";
                foreach (Tile t in lst)
                {
                    s += $"\"{ t.ToString() }\"" + ", ";
                }
                s = s.Substring(0, s.Length - 2) + "]";
                tmp += s + ", ";
            }
            tmp = tmp.Substring(0, tmp.Length - 2) + "]";
            return tmp;
        }

        public Tile getTileAtPos(int m, int n)
        {
            if (m < 0 || m > 7
                || n < 0 || n > 7)
            {
                return null;
            }
            return this.ChessBoard[m][n];
        }

        public void moveBrick(Piece brick, Tile tile)
        {
            Tile tmp = brick.Tile;
            brick.Tile = tile;
            tmp.TilePiece = null;
        }

        public void moveBrickToLegalPos(Piece brick, Tile tile)
        {
            Tile tmp = brick.Tile;
            brick.setLegalTile(tile);
            tmp.TilePiece = null;
        }

        public List<Piece> getBricks(char team)
        {
            List<Piece> lst = new List<Piece>();
            foreach (List<Tile> l in this.ChessBoard)
            {
                foreach (Tile t in l)
                {
                    if (t.TilePiece != null && t.TilePiece.Team == team)
                    {
                        lst.Add(t.TilePiece);
                    }
                }
            }
            return lst;
        }

        public bool checkWin(char team)
        {
            // Small check
            char otherTeam = team == 'W' ? 'B' : 'W';
            Piece king = findFirstBrick<King>(otherTeam);

            List<Tile> lst = this.getLegalTiles(team);

            List<Tile> tiles = new List<Tile>();
            if (king != null)
            {
                tiles.Add(king.Tile);
                tiles.AddRange(king.GetLegalTiles());
            }

            if (tiles.TrueForAll(t => lst.Contains(t)) == false && tiles.Count != 0) {
                return false;
            }


            //Can any move save the king
            foreach (Piece brick in this.getBricks(otherTeam))
            {
                foreach (Tile tile in brick.GetLegalTiles())
                {
                    // Check with genration of new boards
                    Move move = new Move(brick, brick.Tile, tile);
                    Board board = this.generateSuccessor(move);
                    if (board.checkTeamInChess(otherTeam) == false) return false;
                }
            }
            return true;
        }


        internal bool checkTeamInChess(char team)
        {
            Piece king = findFirstBrick<King>(team);
            // Test:: endret til getAllTiles..
            Board b = this;
            return this.getAllTilesUnderAttackBy(team == 'W' ? 'B' : 'W').Contains(king.Tile);
        }

        internal List<Tile> getAllTilesUnderAttackBy(char team)
        {
            List<Piece> bricks = getBricks(team);
            List<Tile> lst = this.getAllTilesUnderAttackFromNoKing(team);

            Piece brick = this.findFirstBrick<King>(team);

            List<Tile> lstKing = new List<Tile>();

            if (brick != null)
            {
                // Possible king moves
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Tile t = this.getTileAtPos(brick.Tile.M + i, brick.Tile.N + j);
                        if (t != null) lstKing.Add(t);
                    }
                }
            }


            // Other king possible moves
            brick = this.findFirstBrick<King>(team == 'W' ? 'B' : 'W');

            List<Tile> OtherLstKing = new List<Tile>();

            if (brick != null)
            {
            for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Tile t = this.getTileAtPos(brick.Tile.M + i, brick.Tile.N + j);
                        if(t != null && lst.Contains(t) == false) OtherLstKing.Add(t);
                    }
                }
            }

            // Joins the lists of the other team´s moves
            List<Tile> attackedFromOtherTeam = this.getAllTilesUnderAttackFromNoKing(team == 'W' ? 'B' : 'W');
            attackedFromOtherTeam.AddRange(OtherLstKing);

            // Find the list of tiles that are attacked by team
            lst.AddRange(lstKing.FindAll(t => attackedFromOtherTeam.Contains(t) == false));

            return lst;
        }


        internal List<Tile> getAllTilesUnderAttackFromNoKing(char team)
        {
            List<Piece> bricks = getBricks(team);
            List<Tile> lst = new List<Tile>();

            foreach (Piece b in bricks)
            {
                if (b is King) continue;
                lst.AddRange(b.getAllTilesUnderAttack());
            }

            return lst;

        }


        private List<Tile> getLegalTiles(char team)
        {
            return this.getAllTilesUnderAttackBy(team).FindAll(t => t.TilePiece == null || (t.TilePiece.Team != team && t.TilePiece is not King));
        }

        private Piece findFirstBrick<T>(char team)
        {
            foreach (List<Tile> lst in this.ChessBoard)
            {
                foreach (Tile t in lst)
                {
                    if (t.TilePiece is T && t.TilePiece.Team == team)
                    {
                        return t.TilePiece;
                    }
                }
            }
            return null;
        }

        public string legalMoves()
        {
            if (this.checkTeamInChess('W'))
            {
                return this.legalMovesTeamInChess('W');
            }
            string str = "{";
            List<Piece> whiteBricks = this.getBricks('W');
            foreach (Piece brick in whiteBricks)
            {
                str += "\"{" + brick.Tile.M + ", " + brick.Tile.N +  "}\": " + this.serilizeMoves(brick.GetLegalTiles()) + ", ";
            }
            str = str.Substring(0, str.Length - 2);
            str += "}";
            return str;
        }

        private string serilizeMoves(List<Tile> tiles)
        {
            string str = "[";
            foreach (Tile t in tiles)
            {
                str += "{" + "\"i\": " + t.M + ", " + "\"j\": "+ t.N + "}, ";
            }
            if(str.Length > 2) str = str.Substring(0, str.Length - 2);
            str += "]";
            return str;

        }

        private string legalMovesTeamInChess(char team)
        {

            string str = "";
            foreach (Piece brick in this.getBricks(team))
            {
                List<Tile> lst = new List<Tile>();
                foreach (Tile tile in brick.GetLegalTiles())
                {
                    // Generation of new boards for checking chess
                    Move move = new Move(brick, brick.Tile, tile);
                    Board board = this.generateSuccessor(move);
                    if (board.checkTeamInChess(team) == false) lst.Add(tile);
                }
                str += "\"{" + brick.Tile.M + ", " + brick.Tile.N + "}\": " + this.serilizeMoves(lst) + ", ";
            }

            str = str.Substring(0, str.Length - 2);
            str += "}";
            return str;
        }

        public List<Move> getAllMoves(char team)
        {
            List<Move> lst = new List<Move>();
            foreach (Piece b in this.getBricks(team))
            {
                foreach (Tile t in b.GetLegalTiles())
                {
                    lst.Add(new Move(b, b.Tile, t));
                }
            }
            return lst;
        }

        public int getScore()
        {
            if (this.checkWin('W'))
            {
                return int.MinValue;
            }
            else if (this.checkWin('B'))
            {
                return int.MaxValue;
            }


            List<Piece> blackTeam = this.getBricks('B');
            List<Piece> whiteTeam = this.getBricks('W');

            int points = 0;

            int max = blackTeam.Count < whiteTeam.Count ? whiteTeam.Count : blackTeam.Count;

            for (int i = 0; i < max; i++)
            {
                if (blackTeam.Count - 1 >= i)
                {
                    if (blackTeam[i] is Queen)
                    {
                        points += 10;
                    }

                    else if (blackTeam[i] is Pawn)
                    {
                        points += 1;
                    }

                    else
                    {
                        points += 5;
                    }
                }

                if (whiteTeam.Count - 1 >= i)
                {
                    if (whiteTeam[i] is Queen)
                    {
                        points -= 10;
                    }

                    else if (whiteTeam[i] is Pawn)
                    {
                        points -= 1;
                    }

                    else
                    {
                        points -= 5;
                    }
                }
            }
            if (points != 0)
            {
                string s = "";
            }

            return points;
        }

        // Use only for legal moves
        public Board generateSuccessor(Move move)
        {

            Board b = this;
            Board board = new Board(this);

            Tile t1 = board.getTileAtPos(move.CurrentTile.M, move.CurrentTile.N);
            Tile t2 = board.getTileAtPos(move.DestionationTile.M, move.DestionationTile.N);

            board.moveBrickToLegalPos(t1.TilePiece, t2);

            return board;
        }


    }
}
