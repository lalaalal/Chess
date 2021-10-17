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
            Judge judge = player.Judge;
            State state = judge.Check(player, this);
            if (state is WrongCommandState)
                return state;

            player.MovePiece(from, to);

            return new PlayingState();
        }

        public Point GetFrom() {
            return from;
        }

        public Point GetTo() {
            return to;
        }
    }
}