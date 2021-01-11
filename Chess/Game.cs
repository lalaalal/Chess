using Chess.Pieces;

namespace Chess
{
    public class Game
    {
        private Board _board = new Board();
        private View view;

        public BoardView Board { get => _board; }
        private Player white;
        private Player black;

        public Game(View view)
        {
            this.view = view;
            white = new Player(Team.White, _board);
            black = new Player(Team.Black, _board);
        }

        public void Play()
        {

        }
    }
}