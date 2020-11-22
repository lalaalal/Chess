namespace Chess.Status
{
    public class WrongCommandState : State
    {
        public WrongCommandState(string message) { Message = message; }

        public string Message { get; private set; }
    }
}