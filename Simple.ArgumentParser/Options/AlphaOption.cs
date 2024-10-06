using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Simple.ArgumentParser.Tests")]

namespace Simple.ArgumentParser.Options;

internal class AlphaOption : Option
{
    protected internal override OptionType Type => OptionType.Alpha;

    internal AlphaOption(string name, char shortName, string description, OptionSettings? settings = null)
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = string.IsNullOrEmpty(value)
            ? $"Value '{value}' is invalid for Argument '{Name}' - expected a value of type '{Type}'"
            : string.Empty;

        return message == string.Empty;
    }
}