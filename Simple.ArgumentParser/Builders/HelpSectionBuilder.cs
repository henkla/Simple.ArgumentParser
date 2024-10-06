using System.Diagnostics;
using System.Text;
using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Builders;

internal class HelpSectionBuilder
{
    internal static string Build(string? description, List<Option> options)
    {
        var tab = new string(' ', Constants.HelpSectionTabLength);
        var executable = Path.GetFileName(Process.GetCurrentProcess().MainModule?.FileName);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Description:");
        stringBuilder.AppendLine($"{tab}{description ?? ".NET Application"}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Usage:");
        stringBuilder.AppendLine($"{tab}{executable ?? "<executable>"} [OPTIONS]");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Options:");

        var longestArgument = options.Max(o => o.Name.Length);
        var longestTypeName = options.Max(o => o.Type.ToString().Length);
        var anyRequired = options.Any(o => o.Settings.Required);
        
        foreach (var option in options)
        {
            var key = $"{tab}{option.Name.PadRight(longestArgument + 2, ' ')}{(option.ShortName != null ? $"{option.ShortName.PadRight(3, ' ')}" : string.Empty)}";
            var type = $"{(option.Type != OptionType.Flag ? $"<{option.Type}>".PadRight(longestTypeName + 4) : "".PadRight(longestTypeName + 4))}";

            stringBuilder.Append($"{key} {type}".PadRight(40, ' '));
            stringBuilder.Append($"{(option.Settings.Required ? "(required)" : "").PadRight(12, ' ')}");
            stringBuilder.Append(option.Description + (option.Type == OptionType.Enumerate ? $" [{string.Join("/", (option as EnumerateOption).AcceptedValues)}]" : string.Empty));
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }
}