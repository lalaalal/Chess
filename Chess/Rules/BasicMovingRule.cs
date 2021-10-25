using Chess.Commands;
using Chess.Pieces;
using Chess.States;

namespace Chess.Rules
{
    public class BasicMovingRule : CommanderRule
    {
        public State Check(Player player, BoardView board, MoveCommand command)
        {
            Point from = command.GetFrom();
            Point to = command.GetTo();
            ImmutablePiece piece = board[from];

            if (piece == null)
                return new WrongCommandState(BoardView.ToChessFormattedString(from)
                        + " is empty");
            if (!Board.IsPointInRange(from) || !Board.IsPointInRange(to))
                return new WrongCommandState(BoardView.ToChessFormattedString(from)
                        + " or " + BoardView.ToChessFormattedString(to)
                        + " is out of range");
            if (player.Team != piece.Team)
                return new WrongCommandState(piece + " is enemy");
            return new PlayingState();
        }
    }
}
