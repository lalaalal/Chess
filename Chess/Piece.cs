using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public enum Team
    {
        BLACK = -1, WHITE = 1
    }

    public enum Kind
    {
        PAWN, 
    }
    public abstract class ImmutablePiece
    {
        public readonly Team team;
        public int x { get => currentPoint.x; }
        public int y { get => currentPoint.y; }

        public Point CurrentPoint { get => new Point(currentPoint); }
        protected Point currentPoint;

        public ImmutablePiece(Point point, Team team)
        {
            this.team = team;
            this.currentPoint = new Point(point);
        }
        public bool IsEnemy(ImmutablePiece piece)
        {
            return piece != null && piece.team != team;
        }

        public abstract bool CanMoveTo(Point newPoint, BoardView board);
    }

    public abstract class Piece : ImmutablePiece
    {
        public Piece(Point point, Team team) : base(point, team)
        {

        }

        public virtual void Move(Point newPoint)
        {
            currentPoint.Move(newPoint);
        }
    }
}
