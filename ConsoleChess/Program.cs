using System;
using Chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            Game game = new Game(new ConsoleView());
            game.Play();
        }
    }
}
