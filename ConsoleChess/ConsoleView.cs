using System;
using Chess;
using Chess.Pieces;
using Chess.Commands;

namespace ConsoleChess
{
    public class ConsoleView : View
    {
        public void Display(BoardView board)
        {
            Console.Clear();
            Console.WriteLine("  a b c d e f g h ");
            for (int y = 7; y >= 0; y--)
            {
                Console.Write(y + 1 + " ");
                for (int x = 0; x < 8; x++)
                {
                    Point point = new Point(x, y);
                    ImmutablePiece piece = board[point];

                    SetColor(point, piece);

                    if (board.IsEmpty(point))
                        Console.Write("  ");
                    else
                        Console.Write(board[point].Identifier + " ");
                }
                SetDefaultColor();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void SetColor(Point point, ImmutablePiece piece)
        {
            if ((point.x + point.y) % 2 == 0)
                Console.BackgroundColor = ConsoleColor.DarkGray;
            else
                Console.BackgroundColor = ConsoleColor.Gray;

            if (piece != null)
            {
                if (piece.Team == Team.White)
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                else
                    Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        private void SetDefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public Command GetCommand(Player player)
        {
            Team team = player.Team;
            Console.Write((team == Team.White ? "WHITE" : "BLACK") + " : ");
            string line = Console.ReadLine();
            return CommandFactory.CreateCommand(line);
        }

        public void Alert(string message)
        {
            Console.WriteLine("! " + message);
        }
    }
}
