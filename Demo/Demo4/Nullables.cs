using System.Diagnostics.CodeAnalysis;

namespace Demo.Demo4;

public static class Demo
{
    public static void DoIt()
    {
        var result = GetIt();
        var nullables = new Nullables(result);

        if (nullables.TryGetUpperCase(out var upper))
        {
            Console.WriteLine(upper.Length);
        }

        string? test1 = null;
        Console.WriteLine(test1);

        string test2 = null;
        Console.WriteLine(test2);
        PrintIt(test2);

        string? test3 = null;
        Console.WriteLine(test3.Length);
    }

    private static string? GetIt()
    {
        return "hello";
    }

    private static void PrintIt(string notNull) => Console.WriteLine(notNull);
}

public class Nullables(string? input)
{
    public int Length => input.Length;

    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis
    public bool TryGetUpperCase([NotNullWhen(true)] out string? upper)
    {
        if (input is null)
        {
            upper = null;
            return false;
        }
        else
        {
            upper = input.ToUpper();
            return true;
        }
    }
}
