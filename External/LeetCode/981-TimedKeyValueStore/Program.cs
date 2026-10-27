Console.WriteLine("Hello, World!");

TimeMap timeMap = new();

timeMap.Set("foo", "bar", 1);
Console.WriteLine("Set foo bar 1");
var getValue = timeMap.Get("foo", 1);
Console.WriteLine($"Get foo 1: {getValue}");

getValue = timeMap.Get("foo", 3);
Console.WriteLine($"Get foo 3: {getValue}");

Console.WriteLine("Set foo bar2 4");
timeMap.Set("foo", "bar2", 4);

getValue = timeMap.Get("foo", 4);
Console.WriteLine($"Get foo 4: {getValue}");

getValue = timeMap.Get("foo", 5);
Console.WriteLine($"Get foo 5: {getValue}");