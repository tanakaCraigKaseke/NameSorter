using NameSorter.Models;

namespace NameSorter.Services;

/// <summary>
/// Sorts by last name, then by the given names (lexicographical, ordinal).
/// </summary>
public sealed class NameSorterService : INameSorter
{
    public IEnumerable<PersonName> Sort(IEnumerable<PersonName> names)
    {
        if (names is null) throw new ArgumentNullException(nameof(names));

        return names
            .OrderBy(n => n.LastName, StringComparer.Ordinal)
            .ThenBy(n => string.Join(' ', n.GivenNames), StringComparer.Ordinal)
            .ToArray(); // materialize to avoid multiple enumeration
    }
}
