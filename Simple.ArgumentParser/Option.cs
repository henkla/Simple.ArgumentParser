namespace Simple.ArgumentParser;

public class Option
{
    internal Option() { }
    
    public string Name { get; init; }
    public string ShortName { get; init; }
    public string Description { get; init; }
    public Type Type { get; init; }
    public bool Required { get; init; }
}