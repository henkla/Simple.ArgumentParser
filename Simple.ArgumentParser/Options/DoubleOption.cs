using System.Globalization;

namespace Simple.ArgumentParser.Options;

internal class DoubleOption : Option
{
    protected internal override OptionType Type => OptionType.Double;

    internal DoubleOption(string name, char shortName, string description, OptionSettings? settings = null)
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = double.TryParse(value, CultureInfo.InvariantCulture, out _)
            ? string.Empty
            : $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}'";

        return message == string.Empty;
    }
}