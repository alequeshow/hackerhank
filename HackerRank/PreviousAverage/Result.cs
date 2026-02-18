public class Result
{
    public static int CountResponseTimeRegressions(List<int> responseTimes)
    {
        var i = 0;
        long currentSum = 0;
        var greaterCount = 0;
        foreach (long item in responseTimes)
        {
            if (i > 0)
            {
                if(item * i > currentSum)
                    greaterCount++;
            }

            currentSum += item;
            i++;
        }

        return greaterCount;
    }
}