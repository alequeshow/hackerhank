using System.Diagnostics;
using System.Reflection;

namespace GanhoCapital.Integration.Tests;

public class IntegrationTests
{
    private readonly string _testCasesPath;
    private readonly string _programPath;

    public IntegrationTests()
    {
        var testDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        _testCasesPath = Path.Combine(testDirectory, "TestCases");
        
        _programPath = Path.Combine(testDirectory, "..", "..", "..", "..", "..", "src", "GanhoCapital");
        _programPath = Path.GetFullPath(_programPath);
    }

    public static IEnumerable<object[]> TestCaseData =>
        new List<object[]>
        {
            new object[] { "Caso1.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0}]" },
            new object[] { "Caso2.txt", "[{\"tax\": 0.0},{\"tax\": 10000.0},{\"tax\": 0.0}]" },
            new object[] { "Caso1e2.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0}]\n[{\"tax\": 0.0},{\"tax\": 10000.0},{\"tax\": 0.0}]" },
            new object[] { "Caso3.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 1000.0}]" },
            new object[] { "Caso4.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0}]" },
            new object[] { "Caso5.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 10000.0}]" },
            new object[] { "Caso6.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 3000.0}]" },
            new object[] { "Caso7.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 3000.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 3700.0},{\"tax\": 0.0}]" },
            new object[] { "Caso8.txt", "[{\"tax\": 0.0},{\"tax\": 80000.0},{\"tax\": 0.0},{\"tax\": 60000.0}]" },
            new object[] { "Caso9.txt", "[{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 0.0},{\"tax\": 1000.0},{\"tax\": 2400.0}]" },
        };

    [Theory]
    [MemberData(nameof(TestCaseData))]
    public async Task TestCase1_ShouldReturnExpectedResult(string testFile, string expectedJsonOutput)
    {
        // Arrange
        var inputFile = Path.Combine(_testCasesPath, testFile);
        
        // Act
        var result = await RunProgramWithInputFileAsync(inputFile);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedJsonOutput, result);
    }

    private async Task<string> RunProgramWithInputFileAsync(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException($"Test case file not found: {inputFilePath}");
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project \"{_programPath}\"",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = _programPath
        };

        using var process = new Process { StartInfo = startInfo };
        var outputBuilder = new List<string>();
        var errorBuilder = new List<string>();

        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                outputBuilder.Add(args.Data);
            }
        };

        process.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                errorBuilder.Add(args.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        // Read the input file and send it to the process
        var inputContent = await File.ReadAllTextAsync(inputFilePath);
        await process.StandardInput.WriteLineAsync(inputContent);
        await process.StandardInput.WriteLineAsync(); // Send empty line to end input
        process.StandardInput.Close();

        await process.WaitForExitAsync();
        
        if (process.ExitCode != 0)
        {
            var errorOutput = string.Join(Environment.NewLine, errorBuilder);
            throw new InvalidOperationException($"Program execution failed with exit code {process.ExitCode}. Error: {errorOutput}");
        }

        var output = string.Join(Environment.NewLine, outputBuilder);
        return output.Trim();
    }
}