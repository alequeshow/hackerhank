namespace TwoStrings
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var q = Convert.ToInt32(Console.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                var s1 = Console.ReadLine();

                var s2 = Console.ReadLine();

                var result = TwoStrings.CheckStrings(s1, s2);

                Console.WriteLine(result);
            }
        }
    }
}