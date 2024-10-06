namespace Simple.ArgumentParser.Options;

internal class EnumerateOption : Option
{
    private readonly string[] _accepted;
    protected internal override OptionType Type => OptionType.Enumerate;
    protected internal IEnumerable<string> AcceptedValues => _accepted;
    
    public EnumerateOption(string name, char shortName, string description, string[] accepted, OptionSettings? settings = null) 
        : base(name, shortName, description, settings)
    {
        _accepted = accepted;
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = _accepted.Contains(value)
            ? string.Empty
            : $"Value '{value}' is invalid for Argument '{Name}' - expected one of: {string.Join(", ", _accepted)}";
        
        return message == string.Empty;
    }
}