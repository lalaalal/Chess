namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public override char Identifier => 'B';

        public override PieceType Type => PieceType.Bishop;

        private static Point DefaultPoint(int x, Team team)
            => new Point(x, team == Team.White ? 0 : 7);

        public Bishop(Point point, Team team) : base(point, team) { }

        /// <summary>
        /// Use default y position by team
        /// </summary>
        public Bishop(int x, Team team)
            : base(DefaultPoint(x, team), team) { }

        protected override bool IsLegalMove(Point delta)
            => (diagonalUnitLength == delta.Unit.Abs);
    }
}
