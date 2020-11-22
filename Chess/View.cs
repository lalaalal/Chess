using Chess.Commands;
using Chess.Pieces;

namespace Chess
{
    public interface View
    {
        void Display(BoardView board);
        void Alert(string message);
        Command GetCommand(Team team);
    }
}