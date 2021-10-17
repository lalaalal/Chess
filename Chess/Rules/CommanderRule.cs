using Chess.Commands;
using Chess.States;

namespace Chess
{
    public interface CommanderRule
    {
        public State Check(Player player, BoardView board, MoveCommand command);
    }
}
