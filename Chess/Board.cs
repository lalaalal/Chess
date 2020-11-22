using System;
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

        protected Piece[,] board = new Piece[8, 8];

        public ImmutablePiece this[Point point]
        {
            get => board[point.y, point.x];
        }

        public bool IsEmpty(Point point)
        {
            return this[point] == null;
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

        public void AddPiece(PieceType kind, Point point, Team team)
        {
            this[point] = Piece.CreatePiece(kind, point, team);
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
            target.Move(to);
            return true;
        }
    }
}
