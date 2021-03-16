using System;
using System.Collections.Generic;
using System.Text;

namespace CoinPathFinder.Debug
{
    internal class InternalLogger : ILoggerService
    {
        public void LogError(string message) =>
            throw new Exception(message);


        public void LogInfo(string message) { }


        public void LogWarning(string message) { }
    }
}
