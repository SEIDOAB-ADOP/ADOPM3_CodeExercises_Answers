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
result += await service.GetPrimesCountAsync(10_000_002, 20_000_000);
timer.Stop();
Console.WriteLine($"Third call took: {timer.ElapsedMilliseconds} ms and found {result} prime numbers.");

// Run the program to see the calculation time without cache.

// Exercise 1:
// Implement caching logic in the PrimeNumberService.GetPrimesCountAsync method
// Use the ConcurrentDictionary<string, PrimeBatch> _primeNumberCache field as cache storage
// The key is created using the CacheKey record (override ToString() method to get string key)
// The value is a PrimeBatch record containing Start, Count and NrPrimes

// Run the program and note the reduced calculation times with cache in place

// Exercise 2:
// Persist the cache to disk using the SerializeJson and DeSerializeJson extension methods
// Store the cache when a new value is added
// Load the cache in the PrimeNumberService constructor, if the file exists. 
// If the files does not exist, start with an empty cache

// Run the program and note the 1st calculation time is reduced with cache persisted to disk in place
