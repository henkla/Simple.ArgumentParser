namespace Simple.ArgumentParser;

internal static class Constants
{
    internal const string Pattern = @"(?<key>--\w+|-[a-zA-Z])(?:\s+(?<value>(?!-[-\w])[^\s]+))?";
    
    internal const string LongPrefix = "--";
    internal const string ShortPrefix = "-";

    internal const string HelpOptionName = "help";
    internal const string VersionOptionName = "version";
    
    internal const int HelpSectionTabLength = 2;
}