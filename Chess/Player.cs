using System;
using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public Team Team { get; set; }

        private Board board;

        public Player(Team team, Board board)
        {
            Team = team;
            this.board = board;

            board.AddPiece(new King(Team));
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
            if (board[from].Team != Team)
                return false;

            return board.MovePiece(from, to);
        }
    }
}