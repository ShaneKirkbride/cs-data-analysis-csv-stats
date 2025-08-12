## Data Analysis Example in C#: CSV Statistics

This C# project demonstrates simple data analysis on a comma‑separated
values (CSV) file. The program reads numeric data from a CSV file and
computes basic statistics—mean, median, standard deviation, minimum
and maximum—for each column. The implementation uses only the .NET
standard library and does not depend on external packages.

### Usage

Compile the `DataAnalysis.cs` file with a C# compiler and run it,
providing the path to a CSV file:

```sh
csc DataAnalysis.cs
mono DataAnalysis.exe data.csv
```

Each column should contain numeric values; non‑numeric entries are
skipped. The program outputs the statistics for each column.

### Example

If `data.csv` contains:

```
A,B,C
1,2,3
4,5,6
7,8,9
```

the program prints:

```
Column 1:
  Mean = 4.0000
  Median = 4.0000
  Std Dev = 3.0000
  Min = 1.0000
  Max = 7.0000
...
```

### Further work

For more comprehensive numerical analysis and data processing,
developers can consider ALGLIB. ALGLIB is a cross‑platform numerical
analysis and data processing library that supports C++ and C# among
other languages. It includes features such as classification,
regression and statistics【709063982044625†L7-L16】, linear algebra and
optimization【709063982044625†L9-L15】, and emphasises portability and
efficiency【709063982044625†L39-L44】. Integrating ALGLIB could add
advanced capabilities like clustering, time series analysis and
optimization to this project.
