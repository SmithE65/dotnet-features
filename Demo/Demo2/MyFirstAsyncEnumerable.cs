

namespace Demo.Demo2;
public class MyFirstAsyncEnumerable
    : IAsyncEnumerable<int>, IAsyncEnumerator<int>
{
    public int Current { get; private set; } = -1;

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (Current < 10)
        {
            Current++;
            await Task.Delay(100);
            return true;
        }
        else
        {
            return false;
        }
    }

    public IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return this;
    }
}
