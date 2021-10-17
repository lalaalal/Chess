namespace Chess.Commands
{
    public interface Command
    {
        States.State Execute(Player player);
    }
}
