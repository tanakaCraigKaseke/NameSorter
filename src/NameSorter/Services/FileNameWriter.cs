using NameSorter.Models;

namespace NameSorter.Services;

public sealed class FileNameWriter(string outputPath) : INameWriter
{
    public void WriteNames(IEnumerable<PersonName> names)
    {
        File.WriteAllLines(outputPath, names.Select(n => n.ToString()));
    }
}
