namespace Demo.Demo1;

public static class Quack
{
    public static void Go()
    {
        foreach (var duck in new Duck())
        {
            Console.WriteLine(duck);
        }
    }
}

public class Duck
{
    private int _count = -1;
    public string Current => "Quack!";

    public Duck GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        return _count++ < 10;
    }
}
