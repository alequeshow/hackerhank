int responseTimesCount = Convert.ToInt32(Console.ReadLine().Trim());

List<int> responseTimes = [];

for (int i = 0; i < responseTimesCount; i++)
{
    int responseTimesItem = Convert.ToInt32(Console.ReadLine().Trim());
    responseTimes.Add(responseTimesItem);
}

int result = Result.CountResponseTimeRegressions(responseTimes);

Console.WriteLine(result);