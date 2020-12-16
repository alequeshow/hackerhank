namespace SherlockAnagrams
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var q = Convert.ToInt32(Console.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                var s = Console.ReadLine();

                var result = new Anagrams(s).SherlockReads();

                Console.WriteLine(result);
            }
        }
    }
}