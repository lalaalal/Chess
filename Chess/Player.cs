using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public Team Team { get; set; }
        public Team Enemy { get => Team == Team.White ? Team.Black : Team.White; }

        private Board board;
        public King King { get; private set; }

        public Judge Judge { get; private set; }

        public Player(Team team, Board board)
        {
            Team = team;
            this.board = board;

            King = new King(Team);
            board.AddPiece(King);
            board.AddPiece(new Queen(Team));

            board.AddPiece(new Rook(0, team));
            board.AddPiece(new Rook(7, team));
            board.AddPiece(new Knight(1, team));
            board.AddPiece(new Knight(6, team));
            board.AddPiece(new Bishop(2, team));
            board.AddPiece(new Bishop(5, team));

            for (int i = 0; i < 8; i++)
                board.AddPiece(new Pawn(i, team));

            Judge = new Judge(board);
        }

        public bool MovePiece(Point from, Point to)
        {
            if (King.IsEnemy(board[from]))
                return false;

            return board.MovePiece(from, to);
        }

    }
}
