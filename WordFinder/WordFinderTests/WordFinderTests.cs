using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WordFinder.WordFinderTests;

public class WordFinderTests
{
    [Fact]
    public void ShouldFindWords_LeftToRight_TopToBottom()
    {
        // Arrange
        var wordFinder = new WordFinder(new List<string>() {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy"
        }); // from requirements data example

        var words = new List<string>() {
            "chill",
            "cold",
            "snow",
            "wind"
        }; // from requirements data example

        // Act
        var wordsFound = wordFinder.Find(words);

        // Assert
        Assert.DoesNotContain("snow", wordsFound);
        Assert.Contains("chill", wordsFound);
        Assert.Contains("cold", wordsFound);
        Assert.Contains("wind", wordsFound);
    }

    [Fact]
    public void ShouldFindWords_ReturnOnlyOnce()
    {
        // Arrange
        var wordFinder = new WordFinder(new List<string>() {
            "abcdc",
            "fgwio",
            "chill", // 1
            "pqnsd",
            "chill"  // 2
        });

        var words = new List<string>() {
            "chill",
            "cold",
        };

        // Act
        var wordsFound = wordFinder.Find(words);

        // Assert
        Assert.True(wordsFound.Count(wf => wf == "chill") == 1);
    }

    [Fact]
    public void ShouldReturnEmptySetOfStrings()
    {
        // Arrange
        var wordFinder = new WordFinder(new List<string>() {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy"
        });

        var words = new List<string>() {
            "dog",
            "cat",
            "lizard",
            "bear"
        };

        // Act
        var wordsFound = wordFinder.Find(words);

        // Assert
        Assert.Equal(new HashSet<string>(), wordsFound);
    }
}
