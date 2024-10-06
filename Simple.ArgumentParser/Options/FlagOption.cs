namespace Simple.ArgumentParser.Options;

internal class FlagOption : Option
{
    protected internal override OptionType Type => OptionType.Flag;

    internal FlagOption(string name, char shortName, string description, OptionSettings settings)
        : base(name, shortName, description, settings)
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        message = value != string.Empty
            ? $"Argument '{Name}' is invalid - expected no value at all'"
            : string.Empty;

        return message == string.Empty;
    }
}