namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public override char Identifier => 'Q';

        private static Point DefaultPoint(Team team)
            => new Point(3, team == Team.White ? 0 : 7);

        public Queen(Point point, Team team) : base(point, team) { }

        /// <summary>
        /// Use default position by team
        /// </summary>
        public Queen(Team team) : base(DefaultPoint(team), team) { }

        protected override bool DoesDirectionCorrect(Point delta)
            => (delta.Abs == diagonalUnitLength || delta.Abs == crossUnitLength);
    }
}