namespace CountingValleys
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var steps = Convert.ToInt32(Console.ReadLine().Trim());

            var path = Console.ReadLine();

            var result = new Result(steps, path).GetValue();

            Console.WriteLine(result);
        }
    }
}