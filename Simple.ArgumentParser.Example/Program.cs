using Simple.ArgumentParser;
using Type = Simple.ArgumentParser.Type;

//args = ["--name", "Simple", "Programmer", "-a", "42", "-c", "false"];

var arguments = new Parser()
    .Options
        .Add(name: "name",
            shortName: "n",
            description: "Your actual name",
            type: Type.Alpha,
            required: true)
        .Add(name: "just-a-flag",
            shortName: "f",
            description: "Just a simple flag",
            type: Type.None,
            required: false)
        .Add(name: "is-cool",
            shortName: "c",
            description: "State whether you are cool or not",
            type: Type.Boolean,
            required: true)
        .Add(name: "super-long-argument",
            shortName: "s",
            description: "This is a really long argument",
            type: Type.Alpha,
            required: false)
        .Add(name: "this-is-an-even-longer-super-long-argument",
            shortName: "t",
            description: "This is a really long argument, on steroids",
            type: Type.Alpha,
            required: false)
        .Add(name: "age",
            shortName: "a",
            description: "Your age",
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