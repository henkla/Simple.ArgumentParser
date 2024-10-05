namespace Simple.ArgumentParser.Options;

internal class HelpOption : Option
{
    protected internal override OptionType Type => OptionType.Flag;
    
    internal HelpOption() : base(Constants.HelpOptionName, 'h', "Show this help section")
    {
    }

    protected internal override bool ValueIsValid(string value, out string message)
    {
        // no-op
        message = string.Empty;
        return true;
    }
}