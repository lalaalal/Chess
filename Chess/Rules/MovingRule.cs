using Chess.Commands;
using Chess.Status;
using Chess.Pieces;

namespace Chess
{
    public class MovingRule : ChessRule {
        public State Check(Player player, BoardView board, MoveCommand command)
        {
            Point from = command.GetFrom();
            Point to = command.GetTo();
            ImmutablePiece piece = board[from];

            if (!Board.IsPointInRange(from) || !Board.IsPointInRange(to))
                return new WrongCommandState(from + " or " + to + " is out of range");
            if (piece == null) 
                return new WrongCommandState(from + " is empty");
            if (player.Team != piece.Team)
                return new WrongCommandState(piece + " is enemy");
            if (!piece.CanMoveTo(command.GetTo(), board))
                return new WrongCommandState(piece + " can't move to " + to);
            Board testBoard = new Board(board);
            testBoard.PopPiece(from);
            if (player.King.IsCheck(testBoard))
                return new WrongCommandState(piece + " can't move to " + to);
            
            
            return new PlayingState();
        }
    }
}
