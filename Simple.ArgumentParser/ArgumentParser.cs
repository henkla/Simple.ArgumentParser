using System.Reflection;
using System.Text.RegularExpressions;
using Simple.ArgumentParser.Builders;
using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser;

public class ArgumentParser
{
    private readonly bool _strictMode;
    private readonly List<Option> _options = [];

    private List<string> _missingCommands = [];
    private readonly List<string> _invalidCommands = [];
    private readonly List<Command> _ignoredCommands = [];
    private readonly List<Command> _validCommands = [];

    private string? _description;
    private string? _version;


    public ArgumentParser(bool strict = false)
    {
        _strictMode = strict;
    }

    public ArgumentParser AddVersionOption(string? version = null)
    {
        _version ??= version ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0";
        TryAddOption(new VersionOption());
        return this;
    }

    public ArgumentParser AddHelpOption(string? description = null)
    {
        _description = description;
        TryAddOption(new HelpOption());
        return this;
    }

    public ArgumentParser AddAlphaOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new AlphaOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }

    public ArgumentParser AddBooleanOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new BooleanOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }

    public ArgumentParser AddFlagOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new FlagOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }

    public ArgumentParser AddDoubleOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new DoubleOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }

    public ArgumentParser AddCharOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new CharOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }
    
    public ArgumentParser AddIntegerOption(string name, char shortName, string description, bool required = false)
    {
        TryAddOption(new IntegerOption(name, shortName, description, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }
    
    public ArgumentParser AddEnumerateOption(string name, char shortName, string description, string[] accepted, bool required = false)
    {
        TryAddOption(new EnumerateOption(name, shortName, description, accepted, new OptionSettings { Required = required, Strict = _strictMode }));
        return this;
    }

    public Args Parse(string[] args)
    {
        // if (_options.Count == 0) return new Args();

        foreach (Match match in Regex.Matches(string.Join(" ", args), Constants.Pattern))
        {
            var kv = GetKeyValue(match);

            var matchingOption = GetMatchingOption(kv.Key);
            if (matchingOption is null)
            {
                _ignoredCommands.Add(new Command
                {
                    Name = kv.Key.TrimStart(Constants.ShortPrefix.ToCharArray()),
                    Value = kv.Value,
                    OptionType = OptionType.Alpha,
                });

                continue;
            }

            if (!matchingOption.ValueIsValid(kv.Value, out var errorMessage))
            {
                _invalidCommands.Add(errorMessage);
                continue;
            }

            _validCommands.Add(new Command
            {
                Value = kv.Value,
                OptionType = matchingOption.Type,
                Name = matchingOption.Name.TrimStart(Constants.ShortPrefix.ToCharArray())
            });
        }

        var parsedArguments = new Args();
        _missingCommands = GetRequiredOptionsMissing();
        RemoveInvalidCommandsFromMissingCommands();

        if (_ignoredCommands.Count > 0)
        {
            parsedArguments.Ignored.AddRange(_ignoredCommands);
        }

        if (_missingCommands.Count > 0)
        {
            parsedArguments.Missing.AddRange(_missingCommands);
        }

        if (_invalidCommands.Count > 0)
        {
            parsedArguments.Invalid.AddRange(_invalidCommands);
        }

        if (_validCommands.Any(a => a.Name == Constants.HelpOptionName))
        {
            parsedArguments.HelpRequested = true;
            parsedArguments.HelpSection = HelpSectionBuilder.Build(_description, _options);
        }

        if (_validCommands.Any(a => a.Name == Constants.VersionOptionName))
        {
            parsedArguments.VersionRequested = true;
            parsedArguments.Version = _version;
        }

        if (_validCommands.Count > 0)
        {
            parsedArguments.Valid = _validCommands;
        }

        return parsedArguments;
    }


    private void RemoveInvalidCommandsFromMissingCommands()
    {
        foreach (var invalidCommand in _invalidCommands)
        {
            var match = Regex.Match(invalidCommand, "'--\\w+'");
            if (match.Success)
            {
                _missingCommands.RemoveAll(m => m.Contains(match.Value));
            }
        }
    }

    private static KeyValuePair<string, string> GetKeyValue(Match match)
    {
        var key = match.Groups["key"].Value;
        var value = match.Groups["value"].Success
            ? match.Groups["value"].Value.Trim()
            : string.Empty;

        return new KeyValuePair<string, string>(key, value);
    }

    private Option? GetMatchingOption(string key)
    {
        return key.StartsWith(Constants.LongPrefix)
            ? _options.SingleOrDefault(o => o.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase))
            : _options.SingleOrDefault(o => o.ShortName.Equals(key, StringComparison.CurrentCultureIgnoreCase));
    }

    private List<string> GetRequiredOptionsMissing()
    {
        return _options
            .Where(o => o.Settings.Required)
            .Select(o => o.Name)
            .Except(_validCommands.Select(a => Constants.LongPrefix + a.Name))
            .Select(a => $"Required argument '{a}' is missing")
            .ToList();
    }

    private void TryAddOption(Option option)
    {
        if (_options.Any(o => o.Name == option.Name))
        {
            throw new ArgumentOptionException($"Option with name '{option.Name}' already exists");
        }
        
        if (_options.Any(o => o.ShortName == option.ShortName))
        {
            throw new ArgumentOptionException($"Option with short name '{option.ShortName}' already exists");
        }
        
        _options.Add(option);
    }
}