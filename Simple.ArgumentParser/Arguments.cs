namespace Simple.ArgumentParser;

public class Arguments
{
    public bool IsValid => InvalidCommands.Count > 0 || MissingCommands.Count > 0;

    public List<Command> ValidCommands { get; set; } = [];
    public List<string> InvalidCommands { get; set; } = [];
    public List<string> MissingCommands { get; set; } = [];
    public bool ShowHelpRequested { get; set; }
    public bool ShowVersionRequested { get; set; }
    public string HelpSection { get; }
    public string Version { get; }

    internal Arguments(Func<string> helpSection, string version)
    {
        HelpSection = helpSection();
        Version = version;
    }
}