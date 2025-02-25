namespace Demo.Demo5;

public static class RawStringLiterals
{
    private static string _someJson = """
        {
            "property": "value"
        }
        """;

    public static string SomeJson => _someJson;

    public static string GetSql(string table, string[] columns)
    {
        return $"""
            SELECT {string.Join(", ", columns)}
            FROM {table}
            """;
    }
}
