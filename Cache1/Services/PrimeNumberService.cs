using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Cache1.Models;
using System.Collections.Concurrent;
using Cache1.Extensions;

namespace Cache1.Services
{
    public class PrimeNumberService
    { 
        ConcurrentDictionary<string, PrimeBatch> _primeNumberCache = new ();

        public async Task<int> GetPrimesCountAsync(int start, int count)
        {
            PrimeBatch pResponse = null;
            var key = new CacheKey(start, count);

            //Check if Cache already contains the value
            if (!_primeNumberCache.TryGetValue(key.ToString(), out pResponse))              
            {
                //If not, calculate the value
                var nrPrimes = await NrPrimesInRangeAsync(start, count);
                pResponse = new PrimeBatch(start, count, nrPrimes);

                //Store the calculated value in the Cache
                _primeNumberCache.TryAdd(key.ToString(), pResponse);

                //Persist the Cache to disk
                _primeNumberCache.SerializeJson("PrimeCache.json");
            }

            return pResponse.NrPrimes;
        }

        private Task<int> NrPrimesInRangeAsync(int start, int count) => Task.Run(() => NrPrimesInRange(start, count));
        private int NrPrimesInRange(int start, int count) => 
            Enumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));


        public PrimeNumberService()
        {
            try
            {
                _primeNumberCache = _primeNumberCache.DeSerializeJson("PrimeCache.json");
            }
            catch (Exception ex)
            {
                _primeNumberCache = new ();
            }
        }
    }
}
