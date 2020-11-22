using System;
using Chess.Pieces;
using Chess.Commands;
using Chess.Status;

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


            while (true)
            {
                view.Display(_board);
                if (NextTurn(Team.White) is GameOverState)
                    break;
                view.Display(_board);
                if (NextTurn(Team.Black) is GameOverState)
                    break;
            }
        }

        public State NextTurn(Team team)
        {
            Player player = (team == Team.White ? white : black);
            State state;

            Command command = view.GetCommand(team);
            state = command.Execute(player);

            while (state is WrongCommandState)
            {
                view.Alert(state.Message);
                command = view.GetCommand(team);
                state = command.Execute(player);
            }

            return state;
        }
    }
}