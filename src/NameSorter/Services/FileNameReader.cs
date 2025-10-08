using NameSorter.Models;

namespace NameSorter.Services;

public sealed class FileNameReader : INameReader
{
    private readonly string _path;
    public FileNameReader(string path) => _path = path;

    public IEnumerable<PersonName> ReadNames()
    {
        if (!File.Exists(_path))
            throw new FileNotFoundException("Input file not found.", _path);

        foreach (var line in File.ReadLines(_path))
        {
            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
                continue;

            yield return PersonName.Parse(trimmed);
        }
    }
}
