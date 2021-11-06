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

        public abstract PieceType Type { get; }

        public int NumMoved { get; protected set; }

        public Point CurrentPoint { get => new Point(_currentPoint); }
        protected Point _currentPoint;

        protected static readonly double diagonalUnitLength = new Point(1, 1).Abs;
        protected static readonly double crossUnitLength = 1.0d;

        public ImmutablePiece(Point point, Team team)
        {
            this.Team = team;
            this._currentPoint = new Point(point);
            this.NumMoved = 0;
        }

        public bool IsFriendly(Team team)
        {
            return this.Team == team;
        }

        public bool IsFriendly(ImmutablePiece piece)
        {
            return piece != null && piece.Team == Team;
        }

        public bool IsEnemy(Team team)
        {
            return this.Team != team;
        }

        public bool IsEnemy(ImmutablePiece piece)
        {
            return piece != null && piece.Team != Team;
        }

        public virtual bool CanMoveTo(Point to, BoardView board)
        {
            Point delta = to - CurrentPoint;

            return IsLegalMove(delta) && IsPathAvailable(to, board);
        }

        public bool IsPathAvailable(Point to, BoardView board)
        {
            if (board.IsEmpty(_currentPoint))
                throw new ArgumentException();
            Point delta = (to - _currentPoint).Unit;

            for (Point p = _currentPoint + delta; p != to; p += delta)
            {
                if (board.IsOccupied(p))
                    return false;
            }
            return board.IsEmpty(to) || IsEnemy(board[to]);
        }

        protected abstract bool IsLegalMove(Point delta);
        public Piece Clone()
        {
            return Piece.CreatePiece(Type, _currentPoint, Team);
        }

        public ImmutablePiece IsInDanger(BoardView board)
        {
            int[] straightDx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] straightDy = { 1, 1, 0, -1, -1, -1, 0, 1 };

            int[] knightDx = { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] knightDy = { 2, 1, -1, -2, -2, -1, 1, 2 };

            int[] pawnDx = { -1, 1 };
            int[] pawnDy = { (int)Team, (int)Team };

            Board.PieceGetter getFirstPieceOnPath = new Board.PieceGetter(board.GetFirstPieceOnPath);
            Board.PieceGetter getPieceAtPoint = new Board.PieceGetter(board.GetPieceAtPoint);

            ImmutablePiece piece = _IsInDanger(straightDx, straightDy, getFirstPieceOnPath, board);
            if (piece != null)
                return piece;
            piece = _IsInDanger(knightDx, knightDy, getPieceAtPoint, board);
            if (piece != null)
                return piece;
            return _IsInDanger(pawnDx, pawnDy, getPieceAtPoint, board);

        }

        private ImmutablePiece _IsInDanger(int[] dx, int[] dy, Board.PieceGetter GetPiece, BoardView board)
        {
            for (int i = 0; i < dx.Length; i++)
            {
                Point delta = new Point(dx[i], dy[i]);
                ImmutablePiece piece = GetPiece(CurrentPoint, delta);

                if (piece == null || IsFriendly(piece))
                    continue;

                if (piece.CanMoveTo(CurrentPoint, board))
                    return piece;
            }

            return null;
        }

        public virtual Piece Copy()
        {
            return Piece.CreatePiece(Type, CurrentPoint, Team);
        }

        public override string ToString()
        {
            return Identifier + BoardView.ToChessFormattedString(CurrentPoint);
        }
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

        public static PieceType ParsePieceType(char c)
        {
            return c switch
            {
                'P' => PieceType.Pawn,
                'N' => PieceType.Knight,
                'B' => PieceType.Bishop,
                'R' => PieceType.Rook,
                'Q' => PieceType.Queen,
                'K' => PieceType.King,
                _ => throw new ArgumentException("Wrong piece type")
            };
        }

        public Piece(Point point, Team team) : base(point, team)
        {

        }

        public virtual void Move(Point to)
        {
            NumMoved++;
            _currentPoint.Move(to);
        }
    }
}
