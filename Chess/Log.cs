using Chess.Pieces;

namespace Chess
{
    public class Log
    {
        public string Message => ToString();
        private ImmutablePiece from;
        private Point to;
        private ImmutablePiece killed;
        private char promote;

        public Log(ImmutablePiece from, Point to, ImmutablePiece killed, char promote = 'P')
        {
            this.from = from.Clone();
            this.to = to;
            this.killed = killed;
            this.promote = promote;
        }

        public override string ToString()
        {
                string msg = from.Identifier.ToString();
                if (killed != null)
                    msg += "x";
                msg += Board.Parse(to);

                if (from is Pawn && promote != 'P')
                    msg += "=" + promote;

                return msg;
        }
    }
}
