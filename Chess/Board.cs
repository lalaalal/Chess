using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public abstract class BoardView
    {
        protected Piece[,] board = new Piece[8, 8];
        public ImmutablePiece this[Point point]
        {
            get => board[point.y, point.x];
        }

        public bool IsEmpty(Point point)
        {
            return this[point] == null;
        }

        public bool IsPathAvailable(Point from, Point to)
        {
            if (IsEmpty(from))
                throw new ArgumentException();
            Point delta = (to - from).Unit();

            for (Point p = from + delta; p != to; p += delta)
            {
                if (!IsEmpty(p))
                    return false;
            }
            return IsEmpty(to) || this[from].IsEnemy(this[to]);
        }

        public static bool PointInRange(Point point)
        {
            return 0 <= point.x && point.x <= 8 && 0 <= point.y && point.y <= 8;
        }
    }

    public class Board : BoardView
    {
        private new Piece this[Point point]
        {
            get => board[point.y, point.x];
            set => board[point.y, point.x] = value;
        }

        public void AddPiece(Point point, Team team, Kind kind)
        {
            this[point] = PieceFactory.CreatePiece(point, team, kind);
        }

        public void AddPiece(Piece piece)
        {
            Point point = piece.CurrentPoint;
            this[point] = piece;
        }

        public bool MovePiece(Point from, Point to)
        {
            Piece target = this[from];
            if (!target.CanMoveTo(to, this))
                return false;

            this[from] = null;
            this[to] = target;
            return true;
        }
    }
}
