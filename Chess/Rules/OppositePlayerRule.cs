using Chess.Status;

namespace Chess.Rules
{
    public interface OppositePlayerRule 
    {
        State Check(Player player, BoardView board);
    }
}