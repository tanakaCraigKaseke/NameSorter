using NameSorter.Models;
using NameSorter.Services;

namespace NameSorter;

public abstract class Program
{
    public static int Main(string[] args)
    {
        // Use default if none provided
        var inputPath = args.Length > 0 ? args[0] : "./src/NameSorter/unsorted-names-list.txt";
        try
        {
            // Check if a file exists before continuing
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return 2;
            }

            INameReader reader = new FileNameReader(inputPath);
            INameWriter writer = new FileNameWriter("sorted-names-list.txt");
            INameSorter sorter = new NameSorterService();

            var names = reader.ReadNames();
            var sorted = sorter.Sort(names);

            var personNames = sorted as PersonName[] ?? sorted.ToArray();
            foreach (var n in personNames)
            {
                Console.WriteLine(n.ToString());
            }

            writer.WriteNames(personNames);

            Console.WriteLine($"\n Sorted names written to sorted-names-list.txt");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 3;
        }
    }
}
