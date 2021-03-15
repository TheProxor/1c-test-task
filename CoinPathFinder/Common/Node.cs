using System.Collections.Generic;


namespace CoinPathFinder.Common
{
    public class Node
    {
        #region Fields

        public Node parent;

        #endregion



        #region Properties

        public HashSet<Node> Neighbours { get; private set; }


        public Point Position { get; private set; }


        public int Value { get; private set; }


        public float G { get; private set; }


        public float H { get; private set; }


        public float Weight { get; private set; }


        public virtual bool IsLocked => false;

        #endregion



        #region Ctor

        public Node(int value, Point position)
        {
            Neighbours = new HashSet<Node>();

            Value = value;
            Position = position;
        }

        #endregion



        #region Methods

        public void SetNodeWeight(float g, float h)
        {
            G = g;
            H = h;

            Weight = G + H;
        }


        public void ClearNodeWeight() =>
            Weight = G = H = 0;

        #endregion
    }
}

