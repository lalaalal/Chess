using Chess.Commands;

namespace Chess
{
    public interface View
    {
        void Display(BoardView board);
        void Alert(string message);
        Command GetCommand(Player player);
    }
}
