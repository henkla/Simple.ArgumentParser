# Simple.ArgumentParser
This is a really simple, yet powerful and dynamic .NET command line argument parser library

## Quick guide

```
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

Now, let's try `--help`:
```
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