using System;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private static Point DefaultPoint(int x, Team team)
            => new Point(x, team == Team.White ? 1 : 6);

        public bool IsMoved { get; private set; }

        public override char Identifier => 'P';

        public Pawn(Point point, Team team) : base(point, team)
        {
            IsMoved = false;
        }

        /// <summary>
        /// Use default y position by team
        /// </summary>
        public Pawn(int x, Team team) : base(DefaultPoint(x, team), team)
        {
            IsMoved = false;
        }

        public override bool CanMoveTo(Point to, BoardView board)
        {
            Point delta = to - _currentPoint;
            if (delta.x == 0)
            {
                if (DoesDirectionCorrect(delta) && board.IsEmpty(to))
                    return true;
                else if (delta.y == (int)Team * 2 && board.IsEmpty(to) && !IsMoved)
                    return true;
                else
                    return false;
            }

            if (Math.Abs(delta.x) == 1 && DoesDirectionCorrect(delta))
                if (!board.IsEmpty(to) && IsEnemy(board[to]))
                    return true;

            return false;
        }

        public override void Move(Point to)
        {
            IsMoved = true;
            base.Move(to);
        }

        protected override bool DoesDirectionCorrect(Point delta)
            => (delta.y == (int)Team);
    }
}