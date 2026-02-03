using System.Collections.Generic;

public class Result
{
    public static int GetTotal(List<int> a, List<int> b)
    {
        var eligibleNumbers = GenerateDivisibles(a, b.Min());        

        var resultNumbers = GetFactors(b, eligibleNumbers);

        return resultNumbers.Count();
    }

    private static IEnumerable<int> GenerateDivisibles(IEnumerable<int> factors, int max)
    {
        var factorParity = 
            factors.All(e => e % 2 == 0) ? 1
            : factors.All(e => e % 2 > 0) ? -1 : 0;

        var eligible = new List<int>();        
        var minValue = 0;

        switch(factorParity)
        {
            case 0: // mixed
            case 1: // all even
                minValue = factors.Min();
                eligible = Enumerable.Range(minValue / 2, max / 2).Select(i => i * 2).ToList();
                break;
            case -1: // all odd
                minValue = factors.Min();
                eligible = Enumerable.Range(minValue, max).ToList();
                break;                
        }

        return GetDivisibles(eligible, factors);
    }

    private static IEnumerable<int> GetDivisibles(IEnumerable<int> source, IEnumerable<int> factors)
    {
        var result = new List<int>();

        foreach(var number in source)
        {
            var valid = factors.All(d => number >= d && number % d == 0);

            if(valid)
                result.Add(number);
        }

        return result;
    }

    private static IEnumerable<int> GetFactors(IEnumerable<int> source, IEnumerable<int> factors)
    {
        var result = new List<int>();

        foreach(var factor in factors)
        {
            var valid = source.All(s => s >= factor && s % factor == 0);

            if(valid)
                result.Add(factor);
        }

        return result;
    }
}