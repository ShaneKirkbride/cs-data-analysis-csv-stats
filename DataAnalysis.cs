/*
 * Data Analysis Tool in C#
 *
 * This program reads a comma‑separated values (CSV) file containing
 * numerical data and computes basic descriptive statistics for each
 * numeric column: mean, median, minimum, maximum and standard
 * deviation. The implementation is self‑contained and uses only the
 * .NET standard library (e.g., System.IO and System.Linq) to avoid
 * external dependencies. It illustrates how to perform simple data
 * analysis tasks in C#.
 *
 * For more comprehensive numerical analysis and data processing,
 * developers can look into libraries like ALGLIB, which provides
 * cross‑platform support for C# and includes functionality such as
 * data analysis (classification/regression, statistics)【709063982044625†L7-L16】,
 * linear algebra, interpolation, and optimization【709063982044625†L9-L15】. ALGLIB
 * offers both free and commercial editions and emphasises portability
 * and efficiency【709063982044625†L39-L44】.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataAnalysis
{
    public class Statistics
    {
        public double Mean { get; set; }
        public double Median { get; set; }
        public double StandardDeviation { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class CsvAnalyzer
    {
        public List<string> Headers { get; private set; } = new List<string>();
        public List<List<double>> Columns { get; private set; } = new List<List<double>>();

        // Load data from a CSV file and populate Headers and Columns
        public void Load(string filePath)
        {
            Columns.Clear();
            Headers.Clear();
            using (var reader = new StreamReader(filePath))
            {
                string? headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    throw new Exception("CSV file is empty.");
                }
                var header = headerLine.Split(',');
                Headers.AddRange(header);
                for (int i = 0; i < header.Length; i++)
                {
                    Columns.Add(new List<double>());
                }
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) break;
                    var parts = line.Split(',');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (double.TryParse(parts[i], NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                        {
                            Columns[i].Add(val);
                        }
                        else
                        {
                            // non‑numeric values are ignored
                        }
                    }
                }
            }
        }

        // Compute statistics for each numeric column
        public List<Statistics> Analyze()
        {
            var results = new List<Statistics>();
            foreach (var col in Columns)
            {
                results.Add(ComputeStats(col));
            }
            return results;
        }

        private static Statistics ComputeStats(List<double> values)
        {
            if (values.Count == 0)
            {
                return new Statistics
                {
                    Mean = double.NaN,
                    Median = double.NaN,
                    StandardDeviation = double.NaN,
                    Min = double.NaN,
                    Max = double.NaN
                };
            }
            values.Sort();
            double mean = values.Average();
            int n = values.Count;
            double median = (n % 2 == 0) ? (values[n / 2 - 1] + values[n / 2]) / 2.0 : values[n / 2];
            double sumSq = values.Select(v => (v - mean) * (v - mean)).Sum();
            double std = Math.Sqrt(sumSq / n);
            return new Statistics
            {
                Mean = mean,
                Median = median,
                StandardDeviation = std,
                Min = values.First(),
                Max = values.Last()
            };
        }
    }

    public static class Program
    {
        // Entry point of program
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DataAnalysis <csv_file_path>");
                return;
            }
            string path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }
            var analyzer = new CsvAnalyzer();
            analyzer.Load(path);
            var statsList = analyzer.Analyze();
            for (int i = 0; i < statsList.Count; i++)
            {
                var stats = statsList[i];
                string columnName = (i < analyzer.Headers.Count && !string.IsNullOrWhiteSpace(analyzer.Headers[i]))
                    ? analyzer.Headers[i]
                    : $"Column {i + 1}";
                Console.WriteLine($"{columnName}:");
                Console.WriteLine($"  Mean = {stats.Mean:F4}");
                Console.WriteLine($"  Median = {stats.Median:F4}");
                Console.WriteLine($"  Std Dev = {stats.StandardDeviation:F4}");
                Console.WriteLine($"  Min = {stats.Min:F4}");
                Console.WriteLine($"  Max = {stats.Max:F4}");
            }
        }
    }
}
