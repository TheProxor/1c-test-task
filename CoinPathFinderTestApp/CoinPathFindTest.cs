using System;
using System.Collections.Generic;
using CoinPathFinder.Common;
using CoinPathFinder.Extensions;
using PathFinder = CoinPathFinder.CoinPathFinder;


namespace CoinPathFinderTestApp
{
    internal class CoinPathFindTest
    {
        #region Fields

        private int[,] cells;

        #endregion



        #region Ctor 

        internal CoinPathFindTest(int[,] cells)
        {
            this.cells = cells.Transpose();
        }

        #endregion



        #region Methods 

        public void RunTest()
        {
            int xLength = cells.GetLength(0);
            int yLength = cells.GetLength(1);

            PathFinder.Initialize(cells, _loggerService: Program.ConsoleLogger);

            List<Node> path = PathFinder.FindPath(new Point(0, 0), new Point(xLength - 1, yLength - 1));

            if (path == null)
            {
                return;
            }

            ShowCellsWithPath(path);

            Space();

            ShowResultCost(path);

            Space();

            ShowResultPath(path);

            Space(5);
        }

        #endregion



        #region Static Methods

        private static void ShowCellsWithPath(List<Node> path)
        {
            for (int y = 0; y < PathFinder.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < PathFinder.Cells.GetLength(0); x++)
                {
                    if (path.Contains(PathFinder.Cells[x, y]))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{PathFinder.Cells[x, y].Value}\t");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{PathFinder.Cells[x, y].Value}\t");
                    }
                }

                Console.WriteLine();
            }
        }


        private static void ShowResultCost(List<Node> path) =>
             Console.WriteLine($"Cost: {path[path.Count - 1].G}");


        private static void ShowResultPath(List<Node> path)
        {
            const string arrowString = " -> ";

            Console.WriteLine("Path:");

            string result = string.Empty;

            foreach (Node node in path)
            {
                result += $"{node.Position}{arrowString}";
            }

            result = result.Remove(result.Length - arrowString.Length, arrowString.Length);

            Console.Write(result);
        }


        private void Space(int lineCount = 1)
        {
            for (int i = 0; i < lineCount; i++)
            {
                Console.WriteLine();
            }
        }

        #endregion
    }
}
