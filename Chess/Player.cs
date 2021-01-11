using System;
using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public Team Team { get; set; }

        private Board board;
        private King king;

        public Player(Team team, Board board)
        {
            Team = team;
            this.board = board;

            king = new King(Team);
            board.AddPiece(king);
            board.AddPiece(new Queen(Team));

            board.AddPiece(new Rook(0, team));
            board.AddPiece(new Rook(7, team));
            board.AddPiece(new Knight(1, team));
            board.AddPiece(new Knight(6, team));
            board.AddPiece(new Bishop(2, team));
            board.AddPiece(new Bishop(5, team));

            for (int i = 0; i < 8; i++)
                board.AddPiece(new Pawn(i, team));
        }

        public bool MovePiece(Point from, Point to)
        {
            if (board.IsEmpty(from))
                return false;
            if (king.IsEnemy(board[from]))
                return false;

            Piece piece = board.PopPiece(from);
            if (IsCheck())
            {
                board.AddPiece(piece);
                return false;
            }


            return board.MovePiece(from, to);
        }

        public bool IsCheck()
        {
            int[] straightDx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] straightDy = { 1, 1, 0, -1, -1, -1, 0, 1 };

            int[] knightDx = { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] knightDy = { 2, 1, -1, -2, -2, -1, 1, 2 };

            int[] pawnDx = { -1, 1 };
            int[] pawnDy = { (int)Team, (int)Team };

            Board.PieceGetter getFirstPieceOnPath = new Board.PieceGetter(board.GetFirstPieceOnPath);
            Board.PieceGetter getPieceAtPoint = new Board.PieceGetter(board.GetPieceAtPoint);

            return _IsCheck(straightDx, straightDy, getFirstPieceOnPath)
                || _IsCheck(knightDx, knightDy, getPieceAtPoint)
                || _IsCheck(pawnDx, pawnDy, getPieceAtPoint);
        }

        private bool _IsCheck(int[] dx, int[] dy, Board.PieceGetter GetPiece)
        {
            for (int i = 0; i < dx.Length; i++)
            {
                Point delta = new Point(dx[i], dy[i]);
                ImmutablePiece piece = GetPiece(king.CurrentPoint, delta);

                if (piece == null || king.IsFriendly(piece))
                    continue;

                if (piece.CanMoveTo(king.CurrentPoint, board))
                    return true;
            }

            return false;
        }
    }
}