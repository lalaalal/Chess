namespace Chess.Pieces
{
    public class King : Piece
    {
        public override char Identifier => 'K';

        public override PieceType Type => PieceType.King;

        public bool IsMoved { get; private set; }

        private static Point DefaultPoint(Team team)
            => new Point(4, team == Team.White ? 0 : 7);

        public King(Point point, Team team, bool isMoved = false) : base(point, team)
        {
            IsMoved = isMoved;
        }

        /// <summary>
        /// Use default position by team
        /// </summary>
        public King(Team team) : base(DefaultPoint(team), team)
        {
            IsMoved = false;
        }

        protected override bool IsLegalMove(Point delta)
            => (diagonalUnitLength == delta.Abs || crossUnitLength == delta.Abs);

        public override void Move(Point to) {
            if (!IsMoved)
                IsMoved = true;
            base.Move(to);
        }

        public override Piece Copy() {
            return new King(CurrentPoint, Team, IsMoved);
        }


        public bool IsCheck(BoardView board)
        {
            int[] straightDx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] straightDy = { 1, 1, 0, -1, -1, -1, 0, 1 };

            int[] knightDx = { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] knightDy = { 2, 1, -1, -2, -2, -1, 1, 2 };

            int[] pawnDx = { -1, 1 };
            int[] pawnDy = { (int)Team, (int)Team };

            Board.PieceGetter getFirstPieceOnPath = new Board.PieceGetter(board.GetFirstPieceOnPath);
            Board.PieceGetter getPieceAtPoint = new Board.PieceGetter(board.GetPieceAtPoint);

            return _IsCheck(straightDx, straightDy, getFirstPieceOnPath, board)
                || _IsCheck(knightDx, knightDy, getPieceAtPoint, board)
                || _IsCheck(pawnDx, pawnDy, getPieceAtPoint, board);
        }

        private bool _IsCheck(int[] dx, int[] dy, Board.PieceGetter GetPiece, BoardView board)
        {
            for (int i = 0; i < dx.Length; i++)
            {
                Point delta = new Point(dx[i], dy[i]);
                ImmutablePiece piece = GetPiece(CurrentPoint, delta);

                if (piece == null || IsFriendly(piece))
                    continue;

                if (piece.CanMoveTo(CurrentPoint, board))
                    return true;
            }

            return false;
        }
    }
}
