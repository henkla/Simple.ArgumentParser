namespace Simple.ArgumentParser.Options;

internal class VersionOption : Option
{
    protected internal override OptionType Type => OptionType.Flag;
    
    internal VersionOption() : base(Constants.VersionOptionName, 'v', "Show application version")
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        // no-op
        message = string.Empty;
        return true;
    }
}