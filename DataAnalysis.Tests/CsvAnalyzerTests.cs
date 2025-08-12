using System;
using System.IO;
using DataAnalysis;

namespace DataAnalysis.Tests;

public class CsvAnalyzerTests
{
    [Fact]
    public void Analyze_ComputesExpectedStatistics()
    {
        string csv = "A,B\n1,2\n4,5\n7,8\n";
        string path = Path.GetTempFileName();
        File.WriteAllText(path, csv);

        var analyzer = new CsvAnalyzer();
        analyzer.Load(path);
        var results = analyzer.Analyze();

        Assert.Equal(2, results.Count);

        var col1 = results[0];
        Assert.Equal(4, col1.Mean, 5);
        Assert.Equal(4, col1.Median, 5);
        Assert.Equal(3, col1.StandardDeviation, 5);
        Assert.Equal(1, col1.Min, 5);
        Assert.Equal(7, col1.Max, 5);

        var col2 = results[1];
        Assert.Equal(5, col2.Mean, 5);
        Assert.Equal(5, col2.Median, 5);
        Assert.Equal(3, col2.StandardDeviation, 5);
        Assert.Equal(2, col2.Min, 5);
        Assert.Equal(8, col2.Max, 5);
    }

    [Fact]
    public void Load_Throws_OnEmptyFile()
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, string.Empty);
        var analyzer = new CsvAnalyzer();
        Assert.Throws<Exception>(() => analyzer.Load(path));
    }

    [Fact]
    public void Load_IgnoresNonNumericValues()
    {
        string csv = "A\n1\ntext\n4\n";
        string path = Path.GetTempFileName();
        File.WriteAllText(path, csv);

        var analyzer = new CsvAnalyzer();
        analyzer.Load(path);
        var results = analyzer.Analyze();

        Assert.Single(results);
        var stats = results[0];
        Assert.Equal(2.5, stats.Mean, 5);
        Assert.Equal(2.5, stats.Median, 5);
        Assert.Equal(Math.Sqrt(4.5), stats.StandardDeviation, 5);
        Assert.Equal(1, stats.Min, 5);
        Assert.Equal(4, stats.Max, 5);
    }
}
