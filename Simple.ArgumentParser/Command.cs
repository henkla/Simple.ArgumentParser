namespace Simple.ArgumentParser;

public class Command
{
    internal Command() { }
    
    public string Name { get; init; }
    public Type Type { get; init; }
    public string Value { get; init; }
}