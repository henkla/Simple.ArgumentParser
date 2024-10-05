using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ProjectA")]

namespace Simple.ArgumentParser.Options;

internal abstract class Option
{
    internal Option(string name, char shortName, string description, OptionSettings? settings = null)
    {
        Name = Constants.LongPrefix + name;
        ShortName = Constants.ShortPrefix + shortName;
        Description = description;
        Settings = settings ?? new OptionSettings();
    }
    
    protected internal string Name { get; }
    protected internal string ShortName { get; }
    protected internal string Description { get; }
    protected internal abstract OptionType Type { get; }
    protected internal OptionSettings Settings { get; }

    protected internal abstract bool ValueIsValid(string value, out string message);
}