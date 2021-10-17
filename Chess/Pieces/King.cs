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

        public override void Move(Point to)
        {
            if (!IsMoved)
                IsMoved = true;
            base.Move(to);
        }

        public override Piece Copy()
        {
            return new King(CurrentPoint, Team, IsMoved);
        }

        public bool IsCheck(BoardView board)
        {
            return IsInDanger(board) != null;
        }
    }
}
