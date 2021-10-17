using Chess.Commands;
using Chess.Status;

namespace Chess
{
    public interface ChessRule
    {
        public State Check(Player player, BoardView board, MoveCommand command);
    }
}
