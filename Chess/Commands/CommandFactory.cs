namespace Chess.Commands
{
    public class CommandFactory
    {
        public static Command CreateCommand(string command)
        {
            Point from = Board.Parse(command.Substring(0, 2));
            Point to = Board.Parse(command.Substring(2, 2));

            return new MoveCommand(from, to);
        }
    }
}