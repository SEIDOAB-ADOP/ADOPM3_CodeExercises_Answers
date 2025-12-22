// See https://aka.ms/new-console-template for more information
namespace Extensions
{
 	public static class StringExtension
	{
		public static string Truncate(this string s, int maxLength, string trucateWith)
        {
            if (string.IsNullOrEmpty(s) || maxLength <= 0) return string.Empty;

            if (s.Length <= maxLength) return s;
            if (trucateWith.Length >= maxLength) return trucateWith.Substring(0, maxLength);

            return s.Substring(0, maxLength - trucateWith.Length) + trucateWith;
        }

   		public static string ToTitleCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(' ', words);
        }        	
    }

    // Extension method for IEnumerable<T> - just like LINQ methods
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeEveryNth<T>(this IEnumerable<T> source, int n)
        {
            int index = 1;
            foreach (var item in source)
            {
                if (index % n == 0)
                    yield return item;
                index++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Extensions!");

            System.Console.WriteLine("--- String Extensions ---");
            string example = "this is an example string for extension methods";
            Console.WriteLine(example.ToTitleCase());
            Console.WriteLine(example.Truncate(10, "..."));

            System.Console.WriteLine("--- Enumerable Extensions ---");
            var numbers = Enumerable.Range(1, 20);
            var everyThird = numbers.TakeEveryNth(3);
            Console.WriteLine(string.Join(", ", everyThird));
        }
    }
}