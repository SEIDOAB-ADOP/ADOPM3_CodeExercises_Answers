using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Newtonsoft.Json;

namespace Cache1.Models
{
    public record PrimeBatch (int Start, int Count, int NrPrimes);
    
    public record CacheKey (int Start, int Count)
    {
        public override string ToString() => $"{Start}_{Count}";
    }
}
