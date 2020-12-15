namespace NewYearChaos
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var t = Convert.ToInt32(Console.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                var n = Convert.ToInt32(Console.ReadLine());

                var q = Array.ConvertAll(Console.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp));

                new QueueControl(q).CheckBribes();
            }
        }
    }
}