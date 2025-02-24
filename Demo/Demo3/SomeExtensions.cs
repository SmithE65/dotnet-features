using System.Runtime.CompilerServices;

namespace Demo.Demo3;

public static class SomeExtensions
{
    public static async IAsyncEnumerable<int> ReadUntilThresholdAsync(
        this IAsyncEnumerable<int> source,
        int threshold,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var count = 0;

        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (item < threshold)
            {
                count++;
                yield return item;
            }
            else
            {
                Console.WriteLine($"{item} broke {threshold} threshold after {count} items");
                break;
            }
        }
    }

    public static async Task<double> AverageAsync(
        this IAsyncEnumerable<int> source,
        CancellationToken cancellationToken = default)
    {
        var values = new List<int>();

        await foreach (var value in source.WithCancellation(cancellationToken))
        {
            values.Add(value);
        }

        return values.Count > 0 ? values.Average() : 0;
    }
}
