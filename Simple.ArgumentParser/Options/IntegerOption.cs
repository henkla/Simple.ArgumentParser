using System.Globalization;

namespace Simple.ArgumentParser.Options;

internal class IntegerOption : Option
{
    protected internal override OptionType Type => OptionType.Integer;

    internal IntegerOption(string name, char shortName, string description, OptionSettings settings)
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = int.TryParse(value, out _)
            ? string.Empty
            : $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}'";
        
        return message == string.Empty;
    }
}