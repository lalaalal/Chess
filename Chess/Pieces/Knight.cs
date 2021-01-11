namespace Chess.Pieces
{
    public class Knight : Piece
    {
        private static readonly double unitLength = new Point(1, 2).Abs;

        public override char Identifier => 'N';

        public override PieceType Type => PieceType.Knight;

        private static Point DefaultPoint(int x, Team team)
            => new Point(x, team == Team.White ? 0 : 7);
        public Knight(Point point, Team team) : base(point, team) { }

        /// <summary>
        /// Use default y position by team
        /// </summary>
        public Knight(int x, Team team) : base(DefaultPoint(x, team), team) { }

        public override bool CanMoveTo(Point to, BoardView board)
        {
            Point delta = to - _currentPoint;

            return IsLegalMove(delta) && IsPointAvailable(to, board);
        }

        private bool IsPointAvailable(Point point, BoardView board)
            => (board.IsEmpty(point) || IsEnemy(board[point]));

        protected override bool IsLegalMove(Point delta)
            => (unitLength == delta.Abs);
    }
}