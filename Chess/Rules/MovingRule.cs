using Chess.Commands;
using Chess.States;
using Chess.Pieces;

namespace Chess.Rules
{
    public class MovingRule : CommanderRule
    {
        public State Check(Player player, BoardView board, MoveCommand command)
        {
            Point from = command.GetFrom();
            Point to = command.GetTo();
            ImmutablePiece piece = board[from];

            if (!piece.CanMoveTo(command.GetTo(), board))
                return new WrongCommandState(piece + " can't move to " + to);
            Board testBoard = new Board(board);
            King testKing = (King)testBoard[player.King.CurrentPoint];
            testBoard.MovePiece(from, to);
            if (testKing.IsCheck(testBoard))
                return new WrongCommandState(piece + " can't move to " + to + " (check)");
            
            return new PlayingState();
        }
    }
}
