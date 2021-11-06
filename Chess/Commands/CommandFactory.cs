using System;
using Chess.Pieces;

namespace Chess.Commands
{
    public class CommandFactory
    {
        public static Command CreateCommand(string command)
        {
            if (command == "exit")
                return new ExitCommand();
            try
            {
                Point from = Board.Parse(command.Substring(0, 2));
                Point to = Board.Parse(command.Substring(2, 2));
                if (command.Length == 5) {
                    PieceType type = Piece.ParsePieceType(command[4]);
                    return new PromotionCommand(from, to, type);
                }

                return new MoveCommand(from, to);
            }
            catch (ArgumentException)
            {
                return new UnknownCommand();
            }
        }
    }
}
