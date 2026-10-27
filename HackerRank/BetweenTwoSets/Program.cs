string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

_ = Convert.ToInt32(firstMultipleInput[0]);

_ = Convert.ToInt32(firstMultipleInput[1]);

var arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

var brr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(brrTemp => Convert.ToInt32(brrTemp)).ToList();

var total = Result.GetTotal(arr, brr);

Console.WriteLine(total);