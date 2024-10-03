using System.Reflection;

namespace Simple.ArgumentParser.Builders;

public class OptionsBuilder
{
    private readonly Parser _parser;
    internal readonly List<Option> options = [];
    internal string? description = ".NET Console Application";
    internal string? version;

    internal OptionsBuilder(Parser parser)
    {
        _parser = parser;
    }

    public OptionsBuilder AddDescription(string description)
    {
        this.description = description;
        return this;
    }

    public OptionsBuilder AddVersion(string? version = null)
    {
        this.version = version ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0";
        var option = new Option { Name = "--version", ShortName = "-v", Description = "Show version information", Type = Type.None, Required = false, };
        options.Add(option);
        return this;
    }

    public OptionsBuilder Add(string name, string shortName, string description, Type type = Type.Alpha, bool required = false)
    {
        var option = new Option { Name = "--" + name, ShortName = "-" + shortName, Description = description, Type = type, Required = required, };
        options.Add(option);
        return this;
    }

    public OptionsBuilder AddHelp()
    {
        var option = new Option { Name = "--help", ShortName = "-h", Description = "Show help section", Type = Type.None, Required = false, };
        options.Add(option);
        return this;
    }

    public Parser Build() => _parser;
}