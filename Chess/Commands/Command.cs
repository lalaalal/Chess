namespace Chess.Commands
{
    public interface Command
    {
        Status.State Execute(Player player);
    }
}
