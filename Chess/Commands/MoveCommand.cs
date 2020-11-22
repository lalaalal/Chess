using System;
using Chess.Status;

namespace Chess.Commands
{
    public class MoveCommand : Command
    {
        private Point from;
        private Point to;

        public MoveCommand(Point from, Point to)
        {
            this.from = from;
            this.to = to;
        }

        public State Execute(Player player)
        {
            try
            {
                if (!player.MovePiece(from, to))
                    return new WrongCommandState("Cannot move " + Board.Parse(from) + " to " + Board.Parse(to));
            }
            catch (NullReferenceException)
            {
                return new WrongCommandState(Board.Parse(from) + " is empty");
            }
            return new PlayingState();
        }
    }
}