namespace Chess.Pieces
{
    public class King : Piece
    {
        public override char Identifier => 'K';

        private static Point DefaultPoint(Team team)
            => new Point(4, team == Team.White ? 0 : 7);
        public King(Point point, Team team) : base(point, team) { }

        /// <summary>
        /// Use default position by team
        /// </summary>
        public King(Team team) : base(DefaultPoint(team), team) { }

        protected override bool DoesDirectionCorrect(Point delta)
            => (diagonalUnitLength == delta.Abs || crossUnitLength == delta.Abs);
    }
}