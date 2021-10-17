using System.Collections.Generic;
using Chess.Pieces;
using Chess.Commands;
using Chess.Status;

namespace Chess
{
    public class Judge
    {
        private BoardView board;
        private List<ChessRule> rules = new List<ChessRule>();

        public Judge(BoardView board)
        {
            this.board = board;

            rules.Add(new MovingRule());
        }

        public void AddRule(ChessRule rule)
        {
            rules.Add(rule);
        }

        public State Check(Player player, MoveCommand cmd)
        {
            State state = new PlayingState();
            foreach (ChessRule rule in rules) {
                state = rule.Check(player, board, cmd);
                if (state is WrongCommandState)
                    return state;
            }
            return state;
        }
    }
}
