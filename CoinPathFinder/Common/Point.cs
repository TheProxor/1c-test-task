namespace CoinPathFinder.Common
{
    public struct Point
    {
        #region Fields

        public readonly int X;
        public readonly int Y;

        #endregion



        #region Ctor

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion



        #region Operators

        public static bool operator ==(Point first, Point second) =>
            first.X == second.X && first.Y == second.Y;


        public static bool operator !=(Point first, Point second) =>
            first.X != second.X || first.Y != second.Y;


        public static Point operator +(Point first, Point second) =>
            new Point(first.X + second.X, first.Y + second.Y);


        public static Point operator -(Point first, Point second) =>
             new Point(first.X - second.X, first.Y - second.Y);

        #endregion



        #region Overrided Methods

        public override string ToString() => 
            $"({X}, {Y})";


        public override bool Equals(object obj) =>
            base.Equals(obj);


        public override int GetHashCode() =>
             base.GetHashCode();

        #endregion
    }
}
