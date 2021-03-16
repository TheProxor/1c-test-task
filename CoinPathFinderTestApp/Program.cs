using System;


namespace CoinPathFinderTestApp
{
    internal class Program
    {
        public static readonly ConsoleLogger ConsoleLogger = new ConsoleLogger();

        private static void Main(string[] args)
        {
            CoinPathFindTest[] coinPathFindTests = new CoinPathFindTest[]
            {

                new CoinPathFindTest(
                    new int[,]
                    {
                        { 0, 0, 1 },
                        { 1, 0, 0 },
                        { 1, 1, 0 },
                    }),

                new CoinPathFindTest(
                    new int[,]
                    {
                        { 0 }
                    }),


                new CoinPathFindTest(
                    new int[,]
                    {
                        { 0, 0, 1 },
                        { 0, 1, 0 },
                        { 0, 1, 1 }
                    }),

                new CoinPathFindTest(
                    new int[,]
                    {
                        { 0, 1, 1, 0, 0, 0 },
                        { 0, 0, 1, 0, 1, 0 },
                        { 1, 0, 0, 0, 1, 0 },
                        { 1, 1, 1, 0, 1, 0 },
                    }),


                 new CoinPathFindTest(GetRandomCells(5, 5)),
                 new CoinPathFindTest(GetRandomCells(8, 8)),
                 new CoinPathFindTest(GetRandomCells(10, 10)),
                 new CoinPathFindTest(GetRandomCells(15, 15)),
            };


            foreach (CoinPathFindTest test in coinPathFindTests)
            {
                test.RunTest();
            }
        }


        private static int[,] GetRandomCells(int xSize, int ySize)
        {
            int[,] cells = new int[xSize, ySize];

            Random random = new Random();

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    cells[x, y] = random.Next(0, 2);
                }
            }

            return cells;
        }
    }
}
