using System;
using System.Collections.Generic;
using System.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Chess.Models;


// {{"board": [["HR", "HKn", "HB", "HK", "HQ", "HB", "HKn", "HR"], ["HP", "HP", "HP", "HP", "HP", "HP", "HP", "HP"], [], [], [], [], ["BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"], ["BR", "BKn", "BB", "BQ", "BK", "BB", "BKn", "BR"]], "moveFromTile": {"i": 1, "j": 0}, "moveToTile": {"i": 2, "j": 0}}}

namespace Chess.Service
{
    public class ChessService
    {
        public string makeMove(JsonElement obj1)
        {
            // Make board and deserialize positions
            JsonObject obj = (JsonObject) JsonObject.Parse(obj1.GetRawText());
            Board board = new Board(obj["board"].ToString());
            JsonObject o1 = (JsonObject) obj["moveFromTile"];
            JsonObject o2 = (JsonObject) obj["moveToTile"];

            // Make move
            Tile t1 = board.getTileAtPos(o1["i"], o1["j"]);
            Piece brick = t1.TilePiece;
            Tile t2 = board.getTileAtPos(o2["i"], o2["j"]);
            board.moveBrick(brick, t2);

            List<Tile> tmp = brick.GetLegalTiles();

            // Check win white
            if (board.checkWin('W'))
            {
                return "{\"winner\": \"white\", " +
                    "\"board\": " + board.ToString() + ", " +
                    "\"moves\": " + "" + "}";
            }

            // Black move
            ChessMinMax algorithm = new ChessMinMax(1);
            Move move = algorithm.getMove('B', board);
            board.moveBrick(move.Brick, move.DestionationTile);

            string legalMoves = board.legalMoves();

            // Check win black
            if (board.checkWin('B'))
            {
                return "{\"winner\": \"black\", " +
                    "\"board\": " + board.ToString() + ", " +
                    "\"moves\": " + "" + "}";
            }

            return "{\"winner\": null, " +
                    "\"board\": " + board.ToString() + ", " +
                    "\"moves\": " + legalMoves + "}";
        }


    }
}
