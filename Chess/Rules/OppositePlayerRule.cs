using Chess.States;

namespace Chess.Rules
{
    public interface OppositePlayerRule 
    {
        State Check(Player player, BoardView board);
    }
}