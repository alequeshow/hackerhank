namespace _2DArrayDS
{
    using System.Collections.Generic;
    using System.Linq;

    public class HourGlass
    {
        private int[][] HourGlassSet;

        public HourGlass(int[][] arr)
        {
            this.HourGlassSet = arr;
        }

        public int ResultSum()
        {
            var sumArray = new List<int>();

            for (var i = 0; i < HourGlassSet.Length; i++)
            {
                for (var j = 0; j < HourGlassSet[i].Length; j++)
                {
                    if (!IsBasePosition(i, j))
                    {
                        continue;
                    }

                    var result = GetHourGlassSum(i, j);

                    sumArray.Add(result);
                }
            }

            return sumArray.Max();
        }

        private bool IsBasePosition(int i, int j)
        {
            //To be base position must be the central top position to retrieve others
            // [i][j-1]     [i][j]    [i][j+1]
            //   ---       [i+1][j]    ---
            // [i+2][j-1]  [i+2][j]  [i+2][j+1]

            // In order to be a valid hourglass, the boundaries arrays must be fullfiled
            // i <= 3
            // 1 <= j <= 4

            return i <= 3 && j > 0 && j <= 4;
        }

        private int GetHourGlassSum(int i, int j)
        {
            var sum = this.HourGlassSet[i][j];
            sum += this.HourGlassSet[i][j - 1];
            sum += this.HourGlassSet[i][j + 1];
            sum += this.HourGlassSet[i + 1][j];
            sum += this.HourGlassSet[i + 2][j - 1];
            sum += this.HourGlassSet[i + 2][j];
            sum += this.HourGlassSet[i + 2][j + 1];

            return sum;
        }
    }
}