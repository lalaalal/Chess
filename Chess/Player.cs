using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public Team Team { get; set; }
        public Team Enemy { get => Team == Team.White ? Team.Black : Team.White; }

        private Board board;

        private King _king;
        public ImmutablePiece King => _king;

        public Judge Judge { get; private set; }

        private Piece[] pawns;

        public Player(Team team, Board board)
        {
            Team = team;
            this.board = board;

            _king = new King(Team);
            board.AddPiece(_king);
            board.AddPiece(new Queen(Team));

            board.AddPiece(new Rook(0, team));
            board.AddPiece(new Rook(7, team));
            board.AddPiece(new Knight(1, team));
            board.AddPiece(new Knight(6, team));
            board.AddPiece(new Bishop(2, team));
            board.AddPiece(new Bishop(5, team));

            pawns = new Piece[8];
            for (int i = 0; i < 8; i++)
            {
                pawns[i] = new Pawn(i, team);
                board.AddPiece(pawns[i]);
            }

            Judge = new Judge(board);
        }

        public bool MovePiece(Point from, Point to)
        {
            if (King.IsEnemy(board[from]))
                return false;

            return board.MovePiece(from, to);
        }

        public void PromotePawn(Point point, PieceType type)
        {
            ImmutablePiece pawn = board[point];
            for (int i = 0; i < 8; i++)
            {
                if (pawns[i].CurrentPoint == point)
                {
                    pawns[i] = Piece.CreatePiece(type, pawn.CurrentPoint, Team);
                    board.PopPiece(pawn.CurrentPoint);
                    board.AddPiece(pawns[i]);
                }
            }
        }
    }
}
