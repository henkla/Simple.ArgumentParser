using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser;

public class Command
{
    internal Command() { }
    
    public string? Name { get; init; }
    public OptionType OptionType { get; init; }
    public string? Value { get; init; }
}