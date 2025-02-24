using System.Collections;
using System.Reflection;

namespace Demo.Demo1;

public class PropertyEnumerator
    : IEnumerable<KeyValuePair<string, object?>>,
    IEnumerator<KeyValuePair<string, object?>>
{
    private readonly KeyValuePair<string, object?>[] _values;
    private int _idx = -1;

    public PropertyEnumerator(object source)
    {
        var flags = BindingFlags.Public
            | BindingFlags.Instance
            | BindingFlags.Static;

        var properties = source
            .GetType()
            .GetProperties(flags);

        KeyValuePair<string, object?> MakePair(PropertyInfo p)
        {
            return new(p.Name, p.GetValue(source));
        }

        _values = [.. properties.Select(MakePair)];
    }

    public KeyValuePair<string, object?> Current => _values[_idx];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        if (_idx < _values.Length - 1)
        {
            _idx++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        _idx = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
