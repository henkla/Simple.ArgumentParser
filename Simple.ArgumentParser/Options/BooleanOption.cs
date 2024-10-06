using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Simple.ArgumentParser.Tests")]

namespace Simple.ArgumentParser.Options;

internal class BooleanOption : Option
{
    protected internal override OptionType Type => OptionType.Boolean;

    internal BooleanOption(string name, char shortName, string description, OptionSettings? settings = null)
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        if (Settings.Strict)
        {
            message = value is "true" or "false"
                ? string.Empty
                : $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}' [true or false]";

            return message == string.Empty;
        }
        
        message = value is "1" or "0" or "true" or "false"
            ? string.Empty
            : $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}' [true or false, 1 or 0]";

        return message == string.Empty;
    }
}