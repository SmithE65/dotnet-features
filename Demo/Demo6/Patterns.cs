namespace Demo.Demo6;

public static class Patterns
{
    public static void Demo1(string? input)
    {
        if (input is null)
        {
            Console.WriteLine("Input was null");
        }
        if (input is not null)
        {
            Console.WriteLine(input);
        }
    }

    public static string Grade(int score) => score switch
    {
        > 98 => "A",
        > 90 => "B",
        > 80 => "C",
        > 65 => "D",
        _ => "F"
    };

    public static string Something(object? input) => input switch
    {
        int and > 0 and < 20 => "Small int, I suppose",
        string { Length: > 10 } => "Long string",
        _ => throw new NotInterestedException()
    };

    public static string SomethingElse(int[] input) => input switch
    {
        { Length: > 5 } => "Too long",
        [1, 2, 3] => "One, two, three!",
        [_, var x, _] => $"{x} in the middle",
        [_, _, _, .. var x] => $"Didn't want the first three and had {x.Length} to go!",
        _ => "Just nope."
    };

    public static Guid Parse(object? input)
    {
        if (input is string { Length: 36 } s)
        {
            return Guid.Parse(s);
        }
        else
        {
            throw new ArgumentException("Whoops", nameof(input));
        }
    }

    public static bool IsBoiling(Temperature temperature) =>
        temperature is { Unit: Unit.F, Value: >= 212 }
        or { Unit: Unit.C, Value: >= 100 }
        or { Unit: Unit.K, Value: >= 373.1 };
}

public record Temperature(double Value, Unit Unit);

public enum Unit
{
    F,
    C,
    K
}

public class NotInterestedException : NotImplementedException;