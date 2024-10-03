using Simple.ArgumentParser;
using Type = Simple.ArgumentParser.Type;

//args = ["--name", "Simple", "Programmer", "-a", "42", "-c", "false"];

var arguments = new Parser()
    .Options
    .Add(name: "key",
        shortName: "k",
        description: "An alphanumeric value",
        type: Type.Alpha,
        required: true)
    .Add(name: "flag",
        shortName: "f",
        description: "Just a simple flag",
        type: Type.None,
        required: false)
    .Add(name: "bool",
        shortName: "b",
        description: "A boolean value (true/false)",
        type: Type.Boolean,
        required: true)
    .Add(name: "a-super-long-option",
        shortName: "l",
        description: "This is a really long option",
        type: Type.Alpha,
        required: false)
    .Add(name: "number",
        shortName: "n",
        description: "An integer value",
        type: Type.Integer,
        required: true)
    .AddHelp()
    .AddVersion()
    .AddDescription("A description of the application.")
    .Build()
    .Parse(args);


// handle help command
if (arguments.ShowHelpRequested)
{
    Console.WriteLine(arguments.HelpSection);
    return 1;
}

// handle version command
if (arguments.ShowVersionRequested)
{
    Console.WriteLine(arguments.Version);
    return 2;
}

// handle invalid commands
if (arguments.InvalidCommands.Count > 0)
{
    arguments.InvalidCommands.ForEach(Console.WriteLine);
    return -2;
}

// handle missing commands
if (arguments.MissingCommands.Count > 0)
{
    arguments.MissingCommands.ForEach(c => Console.WriteLine($"Required command is missing: {c}"));
    return -1;
}

Console.WriteLine($"Hello, {arguments.ValidCommands.Single(c => c.Name == "name").Value}! You look " +
                  $"a lot older than {arguments.ValidCommands.Single(c => c.Name == "age").Value}. " +
                  $"Also, you are {(arguments.ValidCommands.Single(c => c.Name == "is-cool").Value == "true" ? "very" : "not very")} cool.");
Console.WriteLine($"Flag: {arguments.ValidCommands.Any(c => c.Name == "just-a-flag")}");
return 0;