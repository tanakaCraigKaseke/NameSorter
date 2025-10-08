namespace NameSorter.Models;

/// <summary>
/// Represents a person's name where the last token is the surname and the preceding 1-3 tokens are given names.
/// </summary>
public sealed class PersonName : IEquatable<PersonName>
{
    public IReadOnlyList<string> GivenNames { get; }
    public string LastName { get; }

    /// <summary>
    /// Create a PersonName from an enumerable of tokens. The Last token is the last name.
    /// </summary>
    private PersonName(IEnumerable<string> parts)
    {
        var tokens = parts.Where(p => !string.IsNullOrWhiteSpace(p))
                          .Select(p => p.Trim())
                          .ToArray();
        if (tokens.Length < 2)
            throw new ArgumentException("A name must contain at least one given name and a last name.");
        if (tokens.Length > 4)
            throw new ArgumentException("A name may have at most three given names plus a last name.");

        LastName = tokens[^1];
        GivenNames = tokens.Take(tokens.Length - 1).ToArray();
    }

    /// <summary>
    /// Parse a raw line into a PersonName.
    /// </summary>
    public static PersonName Parse(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentException("Name line cannot be empty.");
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new PersonName(parts);
    }

    public override string ToString() => string.Join(' ', GivenNames.Concat(new[] { LastName }));

    public override bool Equals(object? obj) => obj is PersonName other && Equals(other);
    public bool Equals(PersonName? other)
    {
        if (other is null) return false;
        return LastName.Equals(other.LastName, StringComparison.Ordinal)
            && GivenNames.SequenceEqual(other.GivenNames);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(LastName, StringComparer.Ordinal);
        foreach (var g in GivenNames) hash.Add(g, StringComparer.Ordinal);
        return hash.ToHashCode();
    }
}
