using System.Diagnostics;
using Cache1.Services;  

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Cache!");

var service = new PrimeNumberService();
var timer = new Stopwatch();

timer.Start();
var result = await service.GetPrimesCountAsync(2, 10_000_000);
result += await service.GetPrimesCountAsync(10_000_002, 20_000_000);
timer.Stop();
Console.WriteLine($"First call took: {timer.ElapsedMilliseconds} ms and found {result} prime numbers.");

timer.Restart();
result = await service.GetPrimesCountAsync(2, 10_000_000);
result += await service.GetPrimesCountAsync(10_000_002, 20_000_000);
timer.Stop();
Console.WriteLine($"Second call took: {timer.ElapsedMilliseconds} ms and found {result} prime numbers.");

timer.Restart();
result = await service.GetPrimesCountAsync(2, 10_000_000);
timer.Stop();
Console.WriteLine($"Third call took: {timer.ElapsedMilliseconds} ms and found {result} prime numbers.");