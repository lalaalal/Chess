using Chess.States;

namespace Chess.Commands
{
    public class UnknownCommand : Command
    {
        public State Execute(Player player) 
        {
            return new WrongCommandState("Unknown command");
        }
    }
}
