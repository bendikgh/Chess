using System;
using System.Collections.Generic;
using Chess.Models;

namespace Chess.Service
{
    public class ChessMinMax
    {
        public int Depth { get; init; }

        public ChessMinMax(int depth)
        {
            this.Depth = depth;
        }

        public Move getMove(char team, Board board)
        {
            Tuple<int, Move> tmp = this.maxValue(board, 0, int.MinValue, int.MaxValue, team);
            if (tmp.Item1 > 0)
            {
                string s = "";
            }
            return tmp.Item2;
        }

        public Tuple<int, Move> maxValue(Board board, int depth, int alfa, int beta, char team)
        {
            if (this.cutOffTest(board, depth))
            {
                return new Tuple<int, Move>(this.evaluationFunction(board), null);
            }

            int v = int.MinValue;
            Move action = null;

            // Riktig team
            List<Move> moves = board.getAllMoves(team);

            foreach (Move m in moves)
            {
                Tuple<int, Move> tmp = this.minValue(board.generateSuccessor(m), depth + 1, alfa, beta, team);
                if (tmp.Item1 > v)
                {
                    v = tmp.Item1;
                    action = m;
                }

                // Cheks if we can prune the gametree
                if (v > beta)
                {
                    return new Tuple<int, Move>(v, action);
                }
                alfa = alfa > v ? alfa : v;
            }
            return new Tuple<int, Move>(v, action);
        }


        public Tuple<int, Move> minValue(Board board, int depth, int alfa, int beta, char team)
        {
            if (this.cutOffTest(board, depth))
            {
                return new Tuple<int, Move>(this.evaluationFunction(board), null);
            }

            team = team == 'W' ? 'B' : 'W';

            int v = int.MaxValue;
            Move action = null;

            List<Move> moves = board.getAllMoves(team);

            foreach (Move m in moves)
            {
                Tuple<int, Move> tmp = this.maxValue(board.generateSuccessor(m), depth, alfa, beta, team);
                if (tmp.Item1 < v)
                {
                    v = tmp.Item1;
                    action = m;
                }

                // Cheks if we can prune the gametree
                if (v < alfa)
                {
                    return new Tuple<int, Move>(v, action);
                }
                beta = beta > v ? v : beta;
            }
            return new Tuple<int, Move>(v, action);
        }

        public bool cutOffTest(Board board, int depth)
        {
            return board.checkWin('W') || board.checkWin('B') || this.Depth == depth;
        }

        public int evaluationFunction(Board board)
        {
            return board.getScore();
        }
    }
}
