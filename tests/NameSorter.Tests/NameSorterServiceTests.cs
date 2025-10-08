using System;
using System.IO;
using System.Linq;
using NameSorter.Models;
using NameSorter.Services;
using Xunit;

namespace NameSorter.Tests;

public class NameSorterServiceTests
{
    [Fact]
    public void Sorts_By_LastName_Then_GivenNames()
    {
        var input = new []
        {
            PersonName.Parse("Janet Parsons"),
            PersonName.Parse("Vaughn Lewis"),
            PersonName.Parse("Adonis Julius Archer"),
            PersonName.Parse("Shelby Nathan Yoder"),
            PersonName.Parse("Marin Alvarez"),
            PersonName.Parse("London Lindsey"),
            PersonName.Parse("Beau Tristan Bentley"),
            PersonName.Parse("Leo Gardner"),
            PersonName.Parse("Hunter Uriah Mathew Clarke"),
            PersonName.Parse("Mikayla Lopez"),
            PersonName.Parse("Frankie Conner Ritter"),
        };

        INameSorter sorter = new NameSorterService();
        var sorted = sorter.Sort(input).Select(n => n.ToString()).ToArray();

        var expected = new []
        {
            "Marin Alvarez",
            "Adonis Julius Archer",
            "Beau Tristan Bentley",
            "Hunter Uriah Mathew Clarke",
            "Leo Gardner",
            "Vaughn Lewis",
            "London Lindsey",
            "Mikayla Lopez",
            "Janet Parsons",
            "Frankie Conner Ritter",
            "Shelby Nathan Yoder",
        };

        Assert.Equal(expected, sorted);
    }

    [Fact]
    public void PersonName_Validates_Token_Counts()
    {
        Assert.Throws<ArgumentException>(() => PersonName.Parse("Single"));
        Assert.Throws<ArgumentException>(() => PersonName.Parse("A B C D E"));
    }

    [Fact]
    public void Writer_Creates_File_With_Sorted_Names()
    {
        var tmp = Path.Combine(Path.GetTempPath(), $"sorted-names-list-{Guid.NewGuid():N}.txt");
        try
        {
            INameWriter writer = new FileNameWriter(tmp);
            var names = new []
            {
                PersonName.Parse("Marin Alvarez"),
                PersonName.Parse("Adonis Julius Archer"),
            };

            writer.WriteNames(names);

            Assert.True(File.Exists(tmp));
            var lines = File.ReadAllLines(tmp);
            Assert.Equal(new[]{"Marin Alvarez", "Adonis Julius Archer"}, lines);
        }
        finally
        {
            if (File.Exists(tmp)) File.Delete(tmp);
        }
    }
}
