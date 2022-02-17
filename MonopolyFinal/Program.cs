using System;

namespace MonopolyFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.GameInstance.Reset(4, 1000);
            Console.WriteLine(Game.GameInstance.PlayOneGame());
        }
    }
}
