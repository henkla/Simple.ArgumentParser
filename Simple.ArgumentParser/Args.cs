namespace Simple.ArgumentParser;

public class Args
{
    public bool IsValid => !(HasInvalidCommands || HasMissingCommands);
    public bool HasIgnoredCommands => Ignored.Count > 0;
    public bool HasMissingCommands => Missing.Count > 0;
    public bool HasInvalidCommands => Invalid.Count > 0;
    public bool HelpRequested { get; internal set; }
    public bool VersionRequested { get; internal set; }


    public List<Command> Valid { get; } = [];
    public List<Command> Ignored { get; } = [];
    public List<string> Missing { get; } = [];
    public List<string> Invalid { get; } = [];
    public string? HelpSection { get; internal set; }
    public string? Version { get; internal set; }


    internal Args()
    {
    }

    
    public bool Any() => Valid.Count > 0;

    public Command? Get(string key)
    {
        return Valid.SingleOrDefault(c => c.Name == key);
    }

    public List<Command> GetAll() => Valid;
}