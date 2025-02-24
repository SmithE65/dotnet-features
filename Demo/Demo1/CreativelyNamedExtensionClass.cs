using System.Reflection;

namespace Demo.Demo1;

public static class CreativelyNamedExtensionClass
{
    public static IEnumerable<KeyValuePair<string, object?>> EnumerateProperties(this object source)
    {
        return new PropertyEnumerator(source);
    }

    public static IEnumerable<KeyValuePair<string,object?>> ShorterVersion(this object source)
    {
        var flags = BindingFlags.Public
            | BindingFlags.Instance
            | BindingFlags.Static;

        return source
            .GetType()
            .GetProperties(flags)
            .Where(p => p.CanRead)
            .Select(prop => new KeyValuePair<string, object?>(prop.Name, prop.GetValue(source)));
    }

    public static IEnumerable<T> EnumerateBackwards<T>(this T[] source)
    {
        for (int i = source.Length - 1; i >= 0; i--)
        {
            yield return source[i];
        }
    }
}
