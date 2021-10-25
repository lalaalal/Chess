using Chess.Commands;
using Chess.Pieces;
using Chess.States;

namespace Chess.Rules
{
    public class CastlingRule : CommanderRule
    {
        public State Check(Player player, BoardView board, MoveCommand command)
        {
            Point from = command.GetFrom();
            Point to = command.GetTo();
            Point delta = to - from;

            ImmutablePiece king = board[from];

            if (!CheckPoint(to))
                return new WrongCommandState(king + " can't move to " 
                        + BoardView.ToChessFormattedString(to));
            int rookX = to.x == 6 ? 7 : 0;
            ImmutablePiece rook = GetRook(player, board, rookX);

            if (king.Type != PieceType.King)
                return new WrongCommandState(king + " is not a king (castling)");
            if (rook == null)
                return new WrongCommandState(BoardView.ToChessFormattedString(to) 
                        + " is empty or not a rook (castling)");

            if (king.NumMoved != 0)
                return new WrongCommandState(king + " moved more than once (castling)");
            if (rook.NumMoved != 0)
                return new WrongCommandState(rook + " moved more than once (castling)");
            if (player.King.IsInDanger(board) != null)
                return new WrongCommandState("can't castling (check)");

            for (Point p = from + delta.Unit; p != rook.CurrentPoint; p += delta.Unit)
            {
                if (board.IsOccupied(p))
                    return new WrongCommandState(p + " is Occupied (castling)");
                King testPiece = new King(p, player.Team);
                ImmutablePiece blockablePiece = testPiece.IsInDanger(board);
                if (blockablePiece != null)
                    return new WrongCommandState(blockablePiece + " is blocking castling " + p);
            }

            MoveCommand moveCommand = new MoveCommand(rook.CurrentPoint, GetNewRookPoint(rook));
            moveCommand.Execute(player);
            return new PlayingState();
        }

        private ImmutablePiece GetRook(Player player, BoardView board, int x)
        {
            Point pointRook = Rook.DefaultPoint(x, player.Team);
            ImmutablePiece rook = board[pointRook];
            if (rook == null
                    || rook.Type != PieceType.Rook
                    || rook.IsEnemy(player.Team))
                return null;

            return rook;
        }

        private Point GetNewRookPoint(ImmutablePiece piece)
        {
            int newX = piece.CurrentPoint.x == 0 ? 3 : 5;
            return new Point(newX, piece.CurrentPoint.y);
        }

        private bool CheckPoint(Point to)
        {
            return to.x == 6 || to.x == 2;
        }
    }
}
