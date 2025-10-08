using NameSorter.Models;

namespace NameSorter.Services;

public interface INameReader
{
    IEnumerable<PersonName> ReadNames();
}

public interface INameWriter
{
    void WriteNames(IEnumerable<PersonName> names);
}

public interface INameSorter
{
    IEnumerable<PersonName> Sort(IEnumerable<PersonName> names);
}
