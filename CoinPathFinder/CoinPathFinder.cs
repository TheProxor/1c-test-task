using System;
using System.Collections.Generic;
using CoinPathFinder.Common;
using CoinPathFinder.Debug;


namespace CoinPathFinder
{
    public static class CoinPathFinder
    {
        #region Fields

        private static HashSet<Node> openSet;
        private static HashSet<Node> closedSet;

        private static float heuristicMultiplayer;

        private static HeuristicFunction heuristicFunction;

        private static ILoggerService loggerService;

        #endregion



        #region Properties

        public static Node[,] Cells { get; private set; }

        #endregion



        #region Methods
        public static void Initialize(int[,] inputCells,
                                      float _heuristicMultiplayer = 0.5f,
                                      HeuristicFunction _heuristicFunction = HeuristicFunction.ClassicDistance,
                                      ILoggerService _loggerService = default)
        {
            heuristicMultiplayer = _heuristicMultiplayer;
            heuristicFunction = _heuristicFunction;
            loggerService = _loggerService ?? new InternalLogger();


            int xLength = inputCells.GetLength(0);
            int yLength = inputCells.GetLength(1);


            Cells = new Node[xLength, yLength];

            openSet = new HashSet<Node>();
            closedSet = new HashSet<Node>();

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    Node node = new Node(inputCells[x, y], new Point(x, y));

                    if (x > 0)
                    {
                        CreateNeighborhood(node, Cells[x - 1, y]);
                    }

                    if (y > 0)
                    {
                        CreateNeighborhood(node, Cells[x, y - 1]);
                    }

                    Cells[x, y] = node;
                }
            }

            void CreateNeighborhood(Node first, Node second)
            {
                first.Neighbours.Add(second);
                second.Neighbours.Add(first);
            }
        }


        public static List<Node> FindPath(Point startPoint, Point endPoint)
        {
            int xLength = Cells.GetLength(0);
            int yLength = Cells.GetLength(1);

            if (Cells == null || xLength == 0 && yLength == 0)
            {
                loggerService.LogError($"Can't find path before cells initialization.");
                return default;
            }


            Clear();


            if (IsOutOfRange(startPoint))
            {
                loggerService.LogError($"Start point is out of range.");
                return default;
            }

            if (IsOutOfRange(endPoint))
            {
                loggerService.LogError($"End point is out of range.");
                return default;
            }


            Node start = Cells[startPoint.X, startPoint.Y];
            Node end = Cells[endPoint.X, endPoint.Y];


            if (TryFindPath(start, end, out List<Node> path))
            {
                return path;
            }
            else
            {
                loggerService.LogInfo($"Unable to find a path from point {startPoint} to point {endPoint}.");

                return default;
            }
        }


        private static bool TryFindPath(Node from, Node to, out List<Node> path)
        {
            path = default;

            from.SetNodeWeight(0, GetHeuristicPathLength(from, to));

            Node currentNode = from;

            openSet.Add(from);

            while (openSet.Count > 0)
            {
                if (currentNode.Position == to.Position)
                {
                    path = GetFinalPathForNode(currentNode, true);
                    return true;
                }

                CalculateNeighboursWeight(currentNode, to);

                closedSet.Add(currentNode);
                openSet.Remove(currentNode);

                currentNode = FindNodeWithMinWeight();
            }

            return false;
        }


        private static void Clear()
        {
            foreach (Node node in Cells)
            {
                node.ClearNodeWeight();
            }
        }


        private static List<Node> GetFinalPathForNode(Node targetNode, bool isReversed)
        {
            List<Node> result = new List<Node>();

            Node currentNode = targetNode;

            while (currentNode != null)
            {
                result.Add(currentNode);
                currentNode = currentNode.parent;
            }

            if (isReversed)
            {
                result.Reverse();
            }

            return result;
        }


        private static Node FindNodeWithMinWeight()
        {
            Node minNode = null;

            foreach (Node node in openSet)
            {
                if (minNode == null || node.Weight < minNode.Weight)
                {
                    minNode = node;
                }
            }

            return minNode;
        }


        private static void CalculateNeighboursWeight(Node current, Node target)
        {
            foreach (Node node in current.Neighbours)
            {
                if (!node.IsLocked &&
                    !closedSet.Contains(node) &&
                    !openSet.Contains(node))
                {
                    float g = current.G + CalculateStepCost(node, current);
                    float h = GetHeuristicPathLength(current, target);

                    node.SetNodeWeight(g, h);

                    node.parent = current;

                    openSet.Add(node);
                }
            }
        }


        private static float GetHeuristicPathLength(Node from, Node to)
        {
            float result = 0.0f;

            switch (heuristicFunction)
            {
                case HeuristicFunction.ManhattanDistance:
                    result = Math.Abs(to.Position.X - from.Position.X) +
                             Math.Abs(to.Position.Y - from.Position.Y);
                    break;

                case HeuristicFunction.ClassicDistance:
                    result = (float)Math.Sqrt(Math.Pow(to.Position.X - from.Position.X, 2) +
                                              Math.Pow(to.Position.Y - from.Position.Y, 2));
                    break;

                default:
                    loggerService.LogWarning($"This type ({heuristicFunction}) of heuristic function is not supported.");
                    break;
            }

            return result * heuristicMultiplayer;
        }


        private static int CalculateStepCost(Node from, Node to) =>
            from.Value ^ to.Value;


        private static bool IsOutOfRange(Point position)
        {     
            return position.X >= Cells.GetLength(0) ||
                   position.Y >= Cells.GetLength(1) ||
                   position.X < 0 ||
                   position.Y < 0;
        }

        #endregion
    }
}
