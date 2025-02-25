using Demo.Demo1;

var person = new Person("Eric", "Consultant");
var enumerable = CreativelyNamedExtensionClass.EnumerateProperties(person);

foreach (var property in enumerable)
{
    Console.WriteLine(property);
}
