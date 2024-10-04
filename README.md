# Simple.ArgumentParser
This is a really simple, yet powerful and dynamic .NET command line argument parser library.

## Table of Contents
1. [Quick guide](#quick-guide)
   - [Basic setup](#basic-setup)
   - [Usage](#usage)
     - [Overall validity](#overall-validity)
     - [Help section requested](#help-section-requested)
     - [Application version requested](#application-version-requested)
     - [Invalid arguments](#invalid-arguments)
     - [Missing required arguments](#missing-required-arguments)
     - [Valid arguments](#valid-arguments)
2. [Technical information](#technical-information)
3. [Known issues & limitations](#known-issues--limitations)

## Quick guide

### Basic setup

To use the parser, simply set it up to your liking, and pass the _raw arguments_ (the `string[] args`) as shown in the example setup below:

```csharp
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
```

That's it, really.

### Usage

Now, let's try `--help`:

```console
$ <executable> --help

Description:
    A description of the application.

Usage:
    <executable> [OPTIONS]

Options:
    --key                  -k  <Alpha>    (required)  An alphanumeric value
    --flag                 -f                         Just a simple flag
    --bool                 -b  <Boolean>  (required)  A boolean value (true/false)
    --a-super-long-option  -l  <Alpha>                This is a really long option
    --number               -n  <Integer>  (required)  An integer value
    --help                 -h                         Show help section
    --version              -v                         Show version information

```

Okay, that looks simple enough. Let's try using it with some real arguments:

`$ <executable> --key just a key --bool true --number 42`

Now, let's access the parsed arguments. There are many ways to interact with the parsed result. 

#### Overall validity
For instance, you could evaluate the overall status by checking `arguments.IsValid` (returns `false` if either _required arguments are missing_, or _invalid value has been provided_ for an argument - otherwise `true`).

#### Help section requested
There is an easy way of finding out if a user has requested to print the help section (`--help` or `-h`). Check `arguments.ShowHelpRequested` for `true`, and take the opportunity to actually print the help section to the user:

```csharp
// handle help command
if (arguments.ShowHelpRequested)
{
    Console.WriteLine(arguments.HelpSection);
    return;
}
```

#### Application version requested
It's equally easy to find out if user has requested the application version (`--version` or `-v`):

```csharp
// handle version command
if (arguments.ShowVersionRequested)
{
    Console.WriteLine(arguments.Version);
    return;
}
```

#### Invalid arguments
To find out if a user has provided invalid arguments (that is, invalid value for an option), you can check `arguments.InvalidCommands`, which is a list of strings representing a validation message for each invalid argument provided:

```csharp
// handle invalid commands
if (arguments.InvalidCommands.Count > 0)
{
    arguments.InvalidCommands.ForEach(Console.WriteLine);
    return;
}
```

#### Missing required arguments
To find out which - _if any_ - required arguments are missing, you can check `arguments.MissingCommands`, which is a list of strings representing the missing options:

```csharp
// handle missing required commands
if (arguments.MissingCommands.Count > 0)
{
    arguments.MissingCommands.ForEach(c => Console.WriteLine($"Required command is missing: {c}"));
    return;
}
```

#### Valid arguments
Okay, we have now evaluated every aspect of the parsed arguments, except the good part - the _valid arguments_. They reside in a list of `Command`:s named - _yup, you guessed it_ - `ValidCommands`:

```csharp
// just for demo purpose
arguments.ValidCommands.ForEach(c => Console.WriteLine($"Name: {c.Name}, Type: {c.Type}, Value: {c.Value}"));
```

Given no bad or missing input (let's re-use the valid arguments a few paragraphs above), that would give the following output to the Console:

```console
$ <executable> --key just a key --bool true --number 42
Name: key, Type: Alpha, Value: just a key
Name: bool, Type: Boolean, Value: true
Name: number, Type: Integer, Value: 42
```

That's basically it! 🙂

## Technical information

## Known issues & limitations

There are some issues yet to be resolved:
* there's currently no handling of conflicting argument names, be it long or short names. _Be aware of this!_
* at the moment, short names are mandatory for each option. _They should be optional!_
