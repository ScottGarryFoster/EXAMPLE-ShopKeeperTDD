using System;
using CommandManager;

namespace ShopKeeperExample
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            IGameLoop loop = new ShopGameLoop(new ConsoleExchange());
            loop.RunGame();

            return 0;
        }
    }
}