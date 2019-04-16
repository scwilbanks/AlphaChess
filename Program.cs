using System;

namespace AlphaChess
{
    class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
