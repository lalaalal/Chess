using System;

namespace Chess.Pieces
{
    public enum Team
    {
        Black = -1, White = 1
    }

    public enum PieceType
    {
        Pawn = 1, Knight, Bishop, Rook, Queen, King
    }
    public abstract class ImmutablePiece
    {
        public Team Team { get; }
        public int x { get => _currentPoint.x; }
        public int y { get => _currentPoint.y; }

        public abstract char Identifier { get; }

        public Point CurrentPoint { get => new Point(_currentPoint); }
        protected Point _currentPoint;

        protected static readonly double diagonalUnitLength = new Point(1, 1).Abs;
        protected static readonly double crossUnitLength = 1.0d;

        public ImmutablePiece(Point point, Team team)
        {
            this.Team = team;
            this._currentPoint = new Point(point);
        }
        public bool IsEnemy(ImmutablePiece piece)
        {
            return piece != null && piece.Team != Team;
        }

        public virtual bool CanMoveTo(Point to, BoardView board)
        {
            Point delta = to - _currentPoint;

            return DoesDirectionCorrect(delta) && IsPathAvailable(to, board);
        }

        public bool IsPathAvailable(Point to, BoardView board)
        {
            if (board.IsEmpty(_currentPoint))
                throw new ArgumentException();
            Point delta = (to - _currentPoint).Unit;

            for (Point p = _currentPoint + delta; p != to; p += delta)
            {
                if (!board.IsEmpty(p))
                    return false;
            }
            return board.IsEmpty(to) || IsEnemy(board[to]);
        }

        protected abstract bool DoesDirectionCorrect(Point delta);
    }

    public abstract class Piece : ImmutablePiece
    {
        public static Piece CreatePiece(PieceType type, Point point, Team team)
        {
            return type switch
            {
                PieceType.Pawn => new Pawn(point, team),
                PieceType.Knight => new Knight(point, team),
                PieceType.Bishop => new Bishop(point, team),
                PieceType.Rook => new Rook(point, team),
                PieceType.Queen => new Queen(point, team),
                PieceType.King => new King(point, team),
                _ => throw new ArgumentException("Wrong piece type")
            };
        }

        public Piece(Point point, Team team) : base(point, team)
        {

        }

        public virtual void Move(Point to) => _currentPoint.Move(to);
    }
}
