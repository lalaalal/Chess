using Chess.Pieces;
using Chess.States;

namespace Chess.Commands
{
    public class PromotionCommand : MoveCommand
    {
        private PieceType type;

        public PromotionCommand(Point from, Point to, PieceType type) : base(from, to)
        {
            this.type = type;
        }

        public override State Execute(Player player)
        {
            State state = player.Judge.CheckPromotion(player, this);
            if (!(state is PlayingState))
                return state;

            state = base.Execute(player);
            if (!(state is PlayingState))
                return state;
            player.PromotePawn(GetTo(), type);

            return new PlayingState();
        }
    }
}
