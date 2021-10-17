using Chess.States;

namespace Chess.Commands
{
    public class ExitCommand : Command
    {
        public State Execute(Player player)
        {
            return new GameOverState(player.Enemy);
        }

    }
}
