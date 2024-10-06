# Simple.ArgumentParser
This is a really simple, yet powerful and dynamic .NET command line argument parser library.

![GitHub Repo stars](https://img.shields.io/github/stars/henkla/Simple.ArgumentParser)
![GitHub search hit counter](https://img.shields.io/github/search/henkla/Simple.ArgumentParser/goto)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/henkla/Simple.ArgumentParser/nuget-publish.yml)
![GitHub Issues or Pull Requests](https://img.shields.io/github/issues/henkla/Simple.ArgumentParser)
[![NuGet version (Simple.ArgumentParser)](https://img.shields.io/nuget/v/Simple.ArgumentParser.svg?style=flat-square)](https://www.nuget.org/packages/Simple.ArgumentParser/)
![NuGet Downloads](https://img.shields.io/nuget/dt/Simple.ArgumentParser)


## Table of Contents
1. [Quick guide](#quick-guide)
   - [Key points](#key-points)
   - [Basic setup](#basic-setup)
   - [Usage](#usage)
     - [Dynamic help section](#dynamic-help-section)
     - [Overall validity](#overall-validity)
     - [Help section requested](#help-section-requested)
     - [Application version requested](#application-version-requested)
     - [Invalid commands](#invalid-commands)
     - [Missing required commands](#missing-required-commands)
     - [Valid commands](#valid-commands)
     - [Get specific command](#get-specific-command)
3. [Technical information](#technical-information)
4. [Known issues & limitations](#known-issues--limitations)

## Key points
* Super quick and easy
* Supports long (`--long`) and short (`-s`) options
* Type validation:
  - Alphanumeric 
  - Integer
  - Boolean
  - Char
  - Double
  - Enumerated values (basically, you decide what is accepted and not)
  - Flag (arguments without value)
* Help section dynamically generatred
* Supports required arguments
* Catches invalid arguments
* Catches missing arguments
* Catches ignored arguments
* ~Supports default values~ _**(not yet implemented)**_
* ~Suppoert customizable prefixes~ _**(not yet implemented)**_

## Quick guide

### Basic setup

To use the parser, simply set it up to your liking, and pass the _raw arguments_ (the `string[] args`) as shown in the example setup below:

```csharp
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
```

That's it, really.

### Usage

#### Dynamic help section

Now, let's try `--help`:

```console
$ Simple.ArgumentParser.Example.exe --help

Description:
  A description of the application.

Usage:
  Simple.ArgumentParser.Example.exe [OPTIONS]

Options:
  --alpha      -a  <Alpha>                          An alphanumeric option
  --integer    -i  <Integer>                        An integer option
  --boolean    -b  <Boolean>                        A boolean option
  --char       -c  <Char>                           A char option
  --double     -d  <Double>                         A double option
  --enumerate  -e  <Enumerate>                      An enumerate option [accepted-value-1/accepted-value-2]
  --flag       -f                                   A flag option
  --help       -h                                   Show this help section
  --version    -v                                   Show application version


```

Okay, that looks simple enough. Let's try using it with some real arguments:

`$ Simple.ArgumentParser.Example.exe --alpha some-alpha --integer 42 --boolean true --char c double 3.12 --enumerate accepted-value-1 --flag`

Now, let's access the parsed arguments. There are many ways to interact with the parsed result. 

#### Overall validity
For instance, you could evaluate the overall status by checking `arguments.IsValid` (returns `false` if either _required arguments are missing_, or _invalid value has been provided_ for an argument - otherwise `true`).

#### Help section requested
There is an easy way of finding out if a user has requested to print the help section (`--help` or `-h`). Check `arguments.HelpRequested` for `true`, and take the opportunity to actually print the help section to the user:

```csharp
// handle help command
if (arguments.HelpRequested)
{
    Console.WriteLine(arguments.HelpSection);
    return;
}
```

#### Application version requested
It's equally easy to find out if user has requested the application version (`--version` or `-v`):

```csharp
// handle version command
if (arguments.VersionRequested)
{
    Console.WriteLine(arguments.Version);
    return;
}
```

#### Invalid commands
To find out if a user has provided invalid arguments (that is, invalid value for an option), you can check `arguments.HasInvalidCommands`. Returns `true` if and invalid arguments has been provided, otherwise `false`. To access the invalid commands, check `arguments.Invalid` which is a list of strings representing a validation message for each invalid argument provided:

```csharp
// handle invalid commands
if (arguments.HasInvalidCommands)
{
    arguments.Invalid.ForEach(Console.WriteLine);
    return;
}
```

#### Missing required commands
To find out which - _if any_ - required arguments are missing, you can check `arguments.HasMissingCommands`. Returns `true` if there are any required arguments not provided, otherwise `false`. To access the the missing commands, check `arguments.Missing` which is a list of strings representing the missing arguments:

```csharp
// handle missing required commands
if (arguments.HasMissingCommands)
{
    arguments.Missing.ForEach(Console.WriteLine);
    return;
}
```

#### Ignored commands
To find out if there are arguments provided that aren't expected (and therefore ignored), you can check `arguments.HasIgnoredCommands`. Returns `true` if there are any ignored arguments provided, otherwise `false`. To access the the ignored commands, check `arguments.Ignored` which is a list of strings representing the ignored arguments:

```csharp
// handle missing required commands
if (arguments.HasIgnoredCommands)
{
    Console.WriteLine("Ignored commands:");
    arguments.Ignored.ForEach(c => Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
    return;
}
```

#### Valid commands
Okay, we have now evaluated every aspect of the parsed arguments, except the good part - the _valid arguments_. They reside in a list of `Command`:s named - _yup, you guessed it_ - `ValidCommands`:

```csharp
// handle valid commands
if (arguments.IsValid && arguments.Any())
{
    Console.WriteLine("Valid commands:");
    arguments.GetAll().ForEach(c => Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
}
```

Given no bad or missing input (let's re-use the valid arguments a few paragraphs above), that would give the following output to the Console:

```console
Valid commands:
Name: alpha, Type: Alpha, Value: some-alpha
Name: integer, Type: Integer, Value: 42
Name: boolean, Type: Boolean, Value: true
Name: char, Type: Char, Value: c
Name: double, Type: Double, Value: 3.12
Name: enumerate, Type: Enumerate, Value: accepted-value-1
Name: flag, Type: Flag, Value:
```

#### Get specific command

There's a simple way to retrieve a specific command:
```csharp
var specificCommand = arguments.Get("alpha");
Console.WriteLine($"Name: {specificCommand.Name}, Type: {specificCommand.OptionType}, Value: {specificCommand.Value}");
```

If that argument is provided, according to the example above, that would give:

```console
Name: alpha, Type: Alpha, Value: some-alpha
```

That's basically it! 🙂

## Technical information

## Known issues & limitations

There are some issues yet to be resolved:
* ~there's currently no handling of conflicting argument names, be it long or short names. _Be aware of this!_~ _***RESOLVED***_
* at the moment, short names are mandatory for each option. _They should be optional!_
* option prefix should be customizable - currently only `--` and `-` are implemented
