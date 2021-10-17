using Chess.Status;

namespace Chess.Rules
{
    public class CheckRule : OppositePlayerRule
    {
        public State Check(Player player, BoardView board)
        {
            if (player.King.IsCheck(board))
                return new CheckState();
            return new PlayingState();
        }
    }
}