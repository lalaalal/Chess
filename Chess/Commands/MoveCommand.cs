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
            return new PlayingState();
        }
    }
}