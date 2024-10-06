namespace Simple.ArgumentParser.Options;

internal class CharOption : Option
{
    protected internal override OptionType Type => OptionType.Char;
    
    public CharOption(string name, char shortName, string description, OptionSettings? settings = null) 
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = char.TryParse(value, out _)
            ? string.Empty
            : $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}'";

        return message == string.Empty;
    }
}