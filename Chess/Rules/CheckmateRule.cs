using Chess.Status;
using Chess.Commands;
using Chess.Pieces;

namespace Chess.Rules
{
    public class CheckmateRule : OppositePlayerRule
    {
        public State Check(Player player, BoardView board) {
            ImmutablePiece attackingPiece = player.King.IsInDanger(board);
            if (attackingPiece == null)
                return new PlayingState();
            if (CanBlock(player, attackingPiece, board))
                return new CheckState();
            
            return new GameOverState(player.Enemy);
        }

        private bool CanBlock(Player player, ImmutablePiece attackingPiece, BoardView board)
        {
            if (attackingPiece.IsInDanger(board) != null)
                return true;

            Board testBoard = new Board(board);
            King testKing = (King)testBoard[player.King.CurrentPoint];

            int[] straightDx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] straightDy = { 1, 1, 0, -1, -1, -1, 0, 1 };
        
            for (int i = 0; i < straightDx.Length; i++)
            {
                Point org = testKing.CurrentPoint;
                Point to = new Point(straightDx[i], straightDy[i]);
                if (testKing.CanMoveTo(to, board))
                {
                    testBoard.MovePiece(org, to);
                    if (!testKing.IsCheck(testBoard))
                        return true;
                    testBoard.MovePiece(to, org);
                }
            }

            Point delta = (player.King.CurrentPoint - attackingPiece.CurrentPoint).Unit;
            if (delta.Abs == new Point(1, 2).Abs)
                return false;
            for (Point point = attackingPiece.CurrentPoint; point != player.King.CurrentPoint; point += delta)
            {
                King testPiece = new King(point, player.Enemy);
                if (testPiece.IsCheck(board))
                    return true;
            }

            return false;
        }
    }
}
