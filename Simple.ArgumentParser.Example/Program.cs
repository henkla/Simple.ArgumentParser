using Simple.ArgumentParser;

args = [
    "--alpha", "some-alpha", 
    "--integer", "42", 
    "--boolean", "true",
    "--char", "c",
    "--double", "3.12",
    "--enumerate", "accepted-value-1",
    "--flag"];

var arguments = new ArgumentParser()
    .AddAlphaOption(name: "alpha",
        shortName: 'a',
        description: "An alphanumeric option",
        required: false)
    .AddIntegerOption(name: "integer",
        shortName: 'i',
        description: "An integer option",
        required: false)
    .AddBooleanOption(name: "boolean",
        shortName: 'b',
        description: "A boolean option",
        required: false)
    .AddCharOption(name: "char",
        shortName: 'c',
        description: "A char option",
        required: false)
    .AddDoubleOption(name: "double",
        shortName: 'd',
        description: "A double option",
        required: false)
    .AddEnumerateOption(name: "enumerate",
        shortName: 'e',
        description: "An enumerate option",
        ["accepted-value-1", "accepted-value-2"],
        required: false)
    .AddFlagOption(name: "flag",
        shortName: 'f',
        description: "A flag option",
        required: false)
    .AddHelpOption("A description of the application.")
    .AddVersionOption("1.2.3-alpha")
    .Parse(args);

// handle help command
if (arguments.HelpRequested)
{
    Console.WriteLine(arguments.HelpSection);
    return 1;
}

// handle version command
if (arguments.VersionRequested)
{
    Console.WriteLine(arguments.Version);
    return 2;
}

// handle invalid commands
if (arguments.HasInvalidCommands)
{
    arguments.Invalid.ForEach(Console.WriteLine);
}

// handle missing commands
if (arguments.HasMissingCommands)
{
    arguments.Missing.ForEach(Console.WriteLine);
}

// handle ignored commands
if (arguments.HasIgnoredCommands)
{
    Console.WriteLine("Ignored commands:");
    arguments.Ignored.ForEach(c => Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
}

// handle valid commands
if (arguments.IsValid && arguments.Any())
{
    Console.WriteLine("Valid commands:");
    arguments.GetAll().ForEach(c => Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
}

// handle valid commands
var specificCommand = arguments.Get("alpha");
Console.WriteLine($"Name: {specificCommand.Name}, Type: {specificCommand.OptionType}, Value: {specificCommand.Value}");

return 0;