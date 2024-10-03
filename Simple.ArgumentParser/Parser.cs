using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Simple.ArgumentParser.Builders;

namespace Simple.ArgumentParser;

public class Parser
{
    public readonly OptionsBuilder Options;

    public Parser()
    {
        Options = new OptionsBuilder(this);
    }

    public Arguments Parse(string[] args)
    {
        var arguments = new Arguments(BuildHelpSection, Options.version);

        if (Options.options.Count == 0) return arguments;

        const string pattern = @"(?<key>--\w+|-[a-zA-Z])(?:\s+(?<value>[^--]+))?(?=\s+--|\s+-|$)";

        var flatArgs = string.Join(" ", args);
        foreach (Match match in Regex.Matches(flatArgs, pattern))
        {
            // Hämta nyckel och värde
            var key = match.Groups["key"].Value;
            var value = match.Groups["value"].Success
                ? match.Groups["value"].Value.Trim()
                : string.Empty;

            var matchingOption = key.StartsWith("--")
                ? Options.options.SingleOrDefault(o => o.Name == key)
                : Options.options.SingleOrDefault(o => o.ShortName == key);

            if (matchingOption is null) continue;

            var errorMessage = string.Empty;
            switch (matchingOption.Type)
            {
                case Type.Float:
                    ValidateValueType(matchingOption, () => float.TryParse(value, out _), out errorMessage);
                    break;
                case Type.Integer:
                    ValidateValueType(matchingOption, () => int.TryParse(value, out _), out errorMessage);
                    break;
                case Type.Boolean:
                    ValidateValueType(matchingOption, () => bool.TryParse(value, out _), out errorMessage);
                    break;
                case Type.None:
                    ValidateValueType(matchingOption, () => value == string.Empty, out errorMessage);
                    break;
                case Type.Alpha:
                    ValidateValueType(matchingOption, () => value != string.Empty, out errorMessage);
                    break;
            }

            if (errorMessage != string.Empty)
            {
                arguments.InvalidCommands.Add(errorMessage);
                continue;
            }

            arguments.ValidCommands.Add(new Command { Value = value, Type = matchingOption.Type, Name = matchingOption.Name.TrimStart('-') });
        }

        var requiredOptionsMissing = Options.options
            .Where(o => o.Required)
            .Select(o => o.Name)
            .Except(arguments.ValidCommands.Select(a => "--" + a.Name))
            .ToList();

        arguments.MissingCommands.AddRange(requiredOptionsMissing);
        arguments.ShowHelpRequested = arguments.ValidCommands.Any(a => a.Name == "help");
        arguments.ShowVersionRequested = arguments.ValidCommands.Any(a => a.Name == "version");
        return arguments;
    }

    private string BuildHelpSection()
    {
        var executableName = Path.GetFileName(Process.GetCurrentProcess().MainModule?.FileName);
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Description:");
        stringBuilder.AppendLine($"    {Options.description}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Usage:");
        stringBuilder.AppendLine($"    {executableName ?? "<executable>"} [OPTIONS]");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Options:");

        var longestArgument = Options.options.Max(o => o.Name.Length);
        var longestTypeName = Options.options.Max(o => o.Type.ToString().Length);
        
        foreach (var option in Options.options)
        {
            var key = $"    {option.Name.PadRight(longestArgument + 2, ' ')}{(option.ShortName != null ? $"{option.ShortName.PadRight(3, ' ')}" : string.Empty)}";
            var type = $"{(option.Type != Type.None ? $"<{option.Type}>".PadRight(longestTypeName + 4) : "".PadRight(longestTypeName + 4))}";
            
            stringBuilder.Append($"{key} {type}".PadRight(40, ' '));
            stringBuilder.Append($"{(option.Required ? "(required)" : "").PadRight(12, ' ')}");
            stringBuilder.Append(option.Description);
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

    private void ValidateValueType(Option option, Func<bool> validation, out string message)
    {
        message = string.Empty;
        if (validation() is false)
        {
            message = $"Argument '{option.Name}' is invalid - expected value type '{option.Type}'";
        }
    }
}