namespace CoinPathFinder.Extensions
{
    public static class Extensions
    {
        public static int[,] Transpose(this int[,] matrix)
        {
            int[,] result = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }
    }
}
