using System;
using CoinPathFinder.Debug;


namespace CoinPathFinderTestApp
{
    class ConsoleLogger : ILoggerService
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{nameof(CoinPathFinder.CoinPathFinder)}: {message}");
            Console.ResetColor();
        }


        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{nameof(CoinPathFinder.CoinPathFinder)}: {message}");
            Console.ResetColor();
        }


        public void LogInfo(string message)
        {
            Console.WriteLine($"{nameof(CoinPathFinder.CoinPathFinder)}: {message}");
        }
    }
}
