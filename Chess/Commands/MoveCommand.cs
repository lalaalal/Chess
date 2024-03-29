using Chess.States;

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

        public virtual State Execute(Player player)
        {
            Judge judge = player.Judge;
            State state = judge.CheckCommander(player, this);
            if (state is WrongCommandState)
                return state;

            player.MovePiece(from, to);

            return new PlayingState();
        }

        public Point GetFrom() {
            return new Point(from);
        }

        public Point GetTo() {
            return new Point (to);
        }
    }
}
