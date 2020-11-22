using Chess.Status;

namespace Chess.Commands
{
    public interface Command
    {
        State Execute(Player player);
    }
}