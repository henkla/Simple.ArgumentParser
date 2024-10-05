using Simple.ArgumentParser;

//args = ["-t", "hejsan", "-n", "3.1", "-b", "true", "-f", "-fl", "1.3" ];

var arguments = new ArgumentParser()
    .AddAlphaOption(name: "name",
        shortName: 'n',
        description: "Your full name",
        required: false)
    .AddIntegerOption(name: "age",
        shortName: 'a',
        description: "Your age",
        required: false)
    .AddEnumerateOption(name: "sex",
        shortName: 's',
        description: "Enter your sex (male/female/idiot)",
        ["male", "female", "idiot"],
        required: false)
    .AddBooleanOption(name: "happy",
        shortName: 'H',
        description: "If you are happy or not",
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

return 0;