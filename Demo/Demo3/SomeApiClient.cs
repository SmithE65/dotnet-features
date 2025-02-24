namespace Demo.Demo3;

public record ApiResult(bool Success, int[] Values);

public class SomeApiClient : IDisposable
{
    private readonly Random _random;

    public SomeApiClient(Random random)
    {
        Console.WriteLine($"Creating {nameof(SomeApiClient)}");
        _random = random;
    }

    public async Task<ApiResult> GetValuesAsync(
        int take,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(150, cancellationToken);
        var values = Enumerable.Range(0, take).Select(_ => _random.Next(255));
        return new(true, [.. values]);
    }

    #region IDisposable

    private bool _disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~SomeApiClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        Console.WriteLine($"Disposing {nameof(SomeApiClient)}");
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
