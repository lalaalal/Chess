﻿using System;
using Chess.Pieces;

namespace Chess
{
    public abstract class BoardView
    {
        public static Point Parse(string point)
        {
            if ('a' > point[0] || point[0] > 'h')
                throw new ArgumentException("first should be a ~ h");

            if ('1' > point[1] || point[1] > '8')
                throw new ArgumentException("second should be 1 ~ 8");

            int x = point[0] - 'a';
            int y = point[1] - '1';

            return new Point(x, y);
        }

        public static string Parse(Point point)
        {
            return Convert.ToChar('a' + point.x).ToString() + Convert.ToChar('1' + point.y);
        }

        public static string ToChessFormattedString(Point point)
        {
            char x = Convert.ToChar(point.x + 'a');
            char y = Convert.ToChar(point.y + '1');
            return "(" + x + ", " + y + ")";
        }

        protected Piece[,] board = new Piece[8, 8];

        public ImmutablePiece this[Point point]
        {
            get => board[point.y, point.x];
        }

        public bool IsEmpty(Point point)
        {
            return this[point] == null;
        }

        public bool IsOccupied(Point point)
        {
            return this[point] != null;
        }

        public static bool IsPointInRange(Point point)
        {
            return 0 <= point.x && point.x < 8 && 0 <= point.y && point.y < 8;
        }

        public delegate ImmutablePiece PieceGetter(Point from, Point delta);

        public ImmutablePiece GetFirstPieceOnPath(Point from, Point delta)
        {
            for (Point p = from + delta; Board.IsPointInRange(p); p += delta)
            {
                if (IsOccupied(p))
                    return this[p];
            }
            return null;
        }

        public ImmutablePiece GetPieceAtPoint(Point from, Point delta)
        {
            Point p = from + delta;

            if (IsPointInRange(p) && IsOccupied(p))
                return this[p];

            return null;
        }
    }

    public class Board : BoardView
    {
        private new Piece this[Point point]
        {
            get => board[point.y, point.x];
            set => board[point.y, point.x] = value;
        }

        public Board() { }

        public Board(BoardView rhs) {
            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    ImmutablePiece piece = rhs[new Point(x, y)];
                    if (piece == null)
                        board[y, x] = null;
                    else
                        board[y, x] = piece.Copy();

                }
            }
        }

        public void AddPiece(PieceType kind, Point point, Team team)
        {
            this[point] = Piece.CreatePiece(kind, point, team);
        }

        public void AddPiece(Piece piece)
        {
            Point point = piece.CurrentPoint;
            this[point] = piece;
        }

        public Piece PopPiece(Point point)
        {
            this[point] = null;
            return this[point];
        }

        public bool MovePiece(Point from, Point to)
        {
            Piece target = this[from];

            this[from] = null;
            this[to] = target;
            target.Move(to);
            return true;
        }
    }
}
