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
        private CommanderRule basicMovingRule = new BasicMovingRule();

        public Judge(BoardView board)
        {
            this.board = board;

            commanderRules.Add(new MovingRule());
            commanderRules.Add(new CastlingRule());

            oppositeRules.Add(new CheckmateRule());
            // TODO : Stalemate
        }

        public State CheckCommander(Player player, MoveCommand command)
        {
            State state = basicMovingRule.Check(player, board, command);
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

        public State CheckPromotion(Player player, PromotionCommand command)
        {
            Point from = command.GetFrom();
            Point to = command.GetTo();
            if ((player.Team == Team.White && to.y != 7)
                || (player.Team == Team.Black && to.y != 0))
                return new WrongCommandState(BoardView.ToChessFormattedString(to) + "is not the end of board");
            ImmutablePiece pawn = board[from];

            if (pawn.Type != PieceType.Pawn)
                return new WrongCommandState(pawn + " is not a pawn");

            return new PlayingState();
        }
    }
}
