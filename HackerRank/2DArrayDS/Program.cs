namespace _2DArrayDS
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int[][] arr = new int[6][];

            for (int i = 0; i < 6; i++)
            {
                arr[i] = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
            }

            int result = new HourGlass(arr).ResultSum();

            Console.WriteLine(result);
        }
    }
}