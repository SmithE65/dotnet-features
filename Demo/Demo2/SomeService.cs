namespace Demo.Demo2;

public class SomeService(Random random)
{
    private const int BreakingPoint = 250;
    private readonly Random _random = random;

    public async Task<double> DoTheThingWithEnumerables()
    {
        var fetchEnumerable = GetFromApi();
        var threasholdEnumerable = ReadUntilThreshold(fetchEnumerable, BreakingPoint);
        var values = new List<int>();

        await foreach (var value in threasholdEnumerable)
        {
            values.Add(value);
        }

        return values.Count > 0 ? values.Average() : 0;
    }

    private async IAsyncEnumerable<int> GetFromApi()
    {
        while (true)
        {
            using var client = new SomeApiClient(_random);
            var items = await client.GetValuesAsync(10);
            Console.WriteLine($"{nameof(SomeApiClient)} returned {items.Values.Length} items");

            foreach (var item in items.Values)
            {
                yield return item;
            }
        }
    }

    private async IAsyncEnumerable<int> ReadUntilThreshold(
        IAsyncEnumerable<int> source,
        int threshold)
    {
        var count = 0;

        await foreach (var item in source)
        {
            if (item < threshold)
            {
                count++;
                yield return item;
            }
            else
            {
                Console.WriteLine($"{item} broke {BreakingPoint} threashold after {count} items");
                break;
            }
        }
    }

    public async Task<double> DoTheThingWithLoops()
    {
        var itemsToAverage = new List<int>();
        var go = true;

        do
        {
            using var client = new SomeApiClient(_random);
            var items = await client.GetValuesAsync(10);
            Console.WriteLine($"{nameof(SomeApiClient)} returned {items.Values.Length} items");

            foreach (var item in items.Values)
            {
                if (item >= BreakingPoint)
                {
                    Console.WriteLine($"{item} broke {BreakingPoint} threshold after {itemsToAverage.Count} items");
                    go = false;
                    break;
                }
                else
                {
                    itemsToAverage.Add(item);
                }
            }
        } while (go);

        return itemsToAverage.Count > 0
            ? itemsToAverage.Average()
            : 0;
    }
}
