# Simple.ArgumentParser

A lightweight yet powerful and dynamic .NET command-line argument parser library that makes parsing arguments a breeze.

![GitHub Repo stars](https://img.shields.io/github/stars/henkla/Simple.ArgumentParser)
![GitHub search hit counter](https://img.shields.io/github/search/henkla/Simple.ArgumentParser/goto)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/henkla/Simple.ArgumentParser/nuget-publish.yml)
![GitHub Issues or Pull Requests](https://img.shields.io/github/issues/henkla/Simple.ArgumentParser)
[![NuGet version (Simple.ArgumentParser)](https://img.shields.io/nuget/v/Simple.ArgumentParser.svg?style=flat-square)](https://www.nuget.org/packages/Simple.ArgumentParser/)
![NuGet Downloads](https://img.shields.io/nuget/dt/Simple.ArgumentParser)

---

## 📑 Table of Contents

1. [🚀 Quick Guide](#quick-guide)
   - [✨ Key Features](#key-features)
   - [⚙️ Basic Setup](#basic-setup)
   - [📦 Usage](#usage)
2. [🧠 Technical Information](#technical-information)
3. [⚠️ Known Issues & Limitations](#known-issues--limitations)

---

## ✨ Key Features

- Super quick and easy setup
- Supports both short (`-s`) and long (`--long`) option styles
- Built-in type validation for:
  - Alphanumeric
  - Integer
  - Boolean
  - Char
  - Double
  - Enumerated values (with custom allowed values)
  - Flags (value-less switches)
- Automatically generated help section
- Handles:
  - Required arguments
  - Invalid/ignored arguments
  - Missing arguments
- ~Support for default values~ **_(coming soon)_**
- ~Customizable prefixes~ **_(coming soon)_**

---

## ⚙️ Quick Guide

### 🔧 Basic Setup

```csharp
var arguments = new ArgumentParser()
    .AddAlphaOption("alpha", 'a', "An alphanumeric option")
    .AddIntegerOption("integer", 'i', "An integer option")
    .AddBooleanOption("boolean", 'b', "A boolean option")
    .AddCharOption("char", 'c', "A char option")
    .AddDoubleOption("double", 'd', "A double option")
    .AddEnumerateOption("enumerate", 'e', "An enumerate option", ["accepted-value-1", "accepted-value-2"])
    .AddFlagOption("flag", 'f', "A flag option")
    .AddHelpOption("A description of the application.")
    .AddVersionOption("1.2.3-alpha")
    .Parse(args);
```

---

## 📦 Usage

### 📘 Dynamic Help

```console
$ Simple.ArgumentParser.Example.exe --help
```

Prints a clear and comprehensive help section with descriptions.

### ✅ Valid Arguments

```csharp
if (arguments.IsValid && arguments.Any())
{
    Console.WriteLine("Valid commands:");
    arguments.GetAll().ForEach(c =>
        Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
}
```

### ❓ Help or Version Requests

```csharp
if (arguments.HelpRequested)
{
    Console.WriteLine(arguments.HelpSection);
    return;
}

if (arguments.VersionRequested)
{
    Console.WriteLine(arguments.Version);
    return;
}
```

### ❌ Invalid Arguments

```csharp
if (arguments.HasInvalidCommands)
{
    arguments.Invalid.ForEach(Console.WriteLine);
    return;
}
```

### 🚫 Missing Required Arguments

```csharp
if (arguments.HasMissingCommands)
{
    arguments.Missing.ForEach(Console.WriteLine);
    return;
}
```

### ⚠️ Ignored Arguments

```csharp
if (arguments.HasIgnoredCommands)
{
    Console.WriteLine("Ignored commands:");
    arguments.Ignored.ForEach(c =>
        Console.WriteLine($"Name: {c.Name}, Type: {c.OptionType}, Value: {c.Value}"));
}
```

### 🔍 Get Specific Argument

```csharp
var specificCommand = arguments.Get("alpha");
Console.WriteLine($"Name: {specificCommand.Name}, Type: {specificCommand.OptionType}, Value: {specificCommand.Value}");
```

---

## 🧠 Technical Information

_Coming soon._

---

## ⚠️ Known Issues & Limitations

- ~~No handling of conflicting argument names~~ **✅ Resolved**
- Short names currently **required** — should be optional
- Only `--` and `-` prefixes are supported — prefix customization not yet available

---

That's it – Simple.ArgumentParser is ready to make your CLI apps cleaner and easier to maintain!
