using Chess.Pieces;
using System;

namespace Chess.Status
{
    public class GameOverState : State
    {
        public GameOverState(Team team)
        {
            Message = team switch
            {
                Team.White => "White Win",
                Team.Black => "Black Win",
                _ => throw new ArgumentException()
            };
        }
        public string Message { get; }
    }
}