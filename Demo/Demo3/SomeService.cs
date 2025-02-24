namespace Demo.Demo3;

public class SomeService(Random random)
{
    private const int _breakingPoint = 250;
    private readonly Random _random = random;

    #region Loops

    public async Task<double> DoTheThingWithLoops(CancellationToken cancellationToken)
    {
        var itemsToAverage = new List<int>();
        var go = true;

        do
        {
            using var client = new SomeApiClient(_random);
            var items = await client.GetValuesAsync(10, cancellationToken);
            Console.WriteLine($"{nameof(SomeApiClient)} returned {items.Values.Length} items");

            foreach (var item in items.Values)
            {
                if (item >= _breakingPoint)
                {
                    Console.WriteLine($"{item} broke {_breakingPoint} threshold after {itemsToAverage.Count} items");
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

    #endregion

    #region AsyncEnumerable

    public async Task<double> DoTheThingWithEnumerables(CancellationToken cancellationToken)
    {
        var result = await FetchFromApi()
            .ReadUntilThresholdAsync(_breakingPoint, cancellationToken)
            .AverageAsync(cancellationToken);

        return result;
    }

    private async IAsyncEnumerable<int> FetchFromApi()
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

    #endregion
}
