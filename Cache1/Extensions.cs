using System.Collections.Concurrent;
using Newtonsoft.Json;

using Cache1.Models;

namespace Cache1.Extensions;

// Extension method for IEnumerable<T> - just like LINQ methods
public static class PrimeBatchExtensions
{
    
    public static ConcurrentDictionary<string, PrimeBatch> SerializeJson(this ConcurrentDictionary<string, PrimeBatch> list, string jsonFileName)
    {
        string sJson = JsonConvert.SerializeObject(list, Formatting.Indented);

        using (Stream s = File.Create(fname(jsonFileName)))
        using (TextWriter writer = new StreamWriter(s))
            writer.Write(sJson);
        
        return list;
    }

    public static ConcurrentDictionary<string, PrimeBatch> DeSerializeJson(this ConcurrentDictionary<string, PrimeBatch> list, string jsonFileName)
    {
        using (Stream s = File.OpenRead(fname(jsonFileName)))
        using (TextReader reader = new StreamReader(s))
        {
            return JsonConvert.DeserializeObject<ConcurrentDictionary<string, PrimeBatch>>(reader.ReadToEnd());
        }
    }

    static string fname(string name)
    {
        var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        documentPath = Path.Combine(documentPath, "CodeSessions", "PrimesCache");
        if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
        return Path.Combine(documentPath, name);
    }
}

