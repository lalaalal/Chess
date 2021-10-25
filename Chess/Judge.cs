using System.Collections.Generic;
using Chess.Commands;
using Chess.States;
using Chess.Rules;
using Chess.Pieces;

namespace Chess
{
    public class Judge
    {
        private BoardView board;
        private List<CommanderRule> commanderRules = new List<CommanderRule>();
        private List<OppositePlayerRule> oppositeRules = new List<OppositePlayerRule>();
        private CommanderRule basicMovingrule = new BasicMovingRule();

        public Judge(BoardView board)
        {
            this.board = board;

            commanderRules.Add(new MovingRule());
            commanderRules.Add(new CastlingRule());

            oppositeRules.Add(new CheckmateRule());
        }

        public State CheckCommander(Player player, MoveCommand command)
        {
            State state = basicMovingrule.Check(player, board, command);
            if (state is WrongCommandState)
                return state;

            foreach (CommanderRule rule in commanderRules)
            {
                state = rule.Check(player, board, command);
                if (state is PlayingState)
                    return state;
            }
            return state;
        }

        public State CheckOpposite(Player opposite)
        {
            State state = new PlayingState();
            foreach (OppositePlayerRule rule in oppositeRules)
            {
                state = rule.Check(opposite, board);
                if (!(state is PlayingState))
                    return state;
            }
            return state;
        }
    }
}
