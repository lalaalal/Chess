using System.Collections.Generic;
using Chess.Pieces;
using Chess.Commands;
using Chess.States;
using Chess.Rules;

namespace Chess
{
    public class Judge
    {
        private BoardView board;
        private List<CommanderRule> commanderRules = new List<CommanderRule>();
        private List<OppositePlayerRule> oppositeRules = new List<OppositePlayerRule>();

        public Judge(BoardView board)
        {
            this.board = board;

            commanderRules.Add(new MovingRule());

            oppositeRules.Add(new CheckmateRule());
        }

        public State CheckCommander(Player player, MoveCommand cmd)
        {
            State state = new PlayingState();
            foreach (CommanderRule rule in commanderRules) {
                state = rule.Check(player, board, cmd);
                if (!(state is PlayingState))
                    return state;
            }
            return state;
        }

        public State CheckOpposite(Player opposite)
        {
            State state = new PlayingState();
            foreach (OppositePlayerRule rule in oppositeRules) {
                state = rule.Check(opposite, board);
                if (!(state is PlayingState))
                    return state;
            }
            return state;
        }
    }
}
