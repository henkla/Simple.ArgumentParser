namespace Simple.ArgumentParser.Tests;

public class ArgumentParserTests
{
    [Fact]
    public void Parse_should_handle_expected_arguments()
    {
        // arrange

        string[] args = [ 
            "--alpha", "value",
            "--boolean", "true",
            "--char", "c",
            "--double", "3.14",
            "--enumerate", "a",
            "--flag",
            "--integer", "42",
            "--help",
            "--version"
        ];
        
        var subjectUnderTest = new ArgumentParser()
            .AddAlphaOption("alpha", 'a', "description")
            .AddBooleanOption("boolean", 'b', "description")
            .AddCharOption("char", 'c', "description")
            .AddDoubleOption("double", 'd', "description")
            .AddEnumerateOption("enumerate", 'e', "description", ["a", "b", "c"])
            .AddFlagOption("flag", 'f', "description")
            .AddIntegerOption("integer", 'i', "description")
            .AddVersionOption()
            .AddHelpOption();

        // act

        var result = subjectUnderTest.Parse(args);

        // assert

        Assert.NotNull(result);
        Assert.True(result.HelpRequested);
        Assert.True(result.VersionRequested);
        Assert.True(result.IsValid);
        Assert.False(result.HasMissingCommands);
        Assert.False(result.HasIgnoredCommands);
        Assert.False(result.HasInvalidCommands);
        Assert.Equal(result.Valid.Count, 9);
        Assert.NotNull(result.Version);
        Assert.NotEmpty(result.Version);
        Assert.NotNull(result.HelpSection);
        Assert.NotEmpty(result.HelpSection);
        Assert.NotNull(result.Get("alpha"));
    }

    [Fact]
    public void Parse_should_ignore_unexpected_arguments()
    {
        // arrange

        var args = new[] { "--help" };
        
        var subjectUnderTest = new ArgumentParser();
        
        // act

        var result = subjectUnderTest.Parse(args);
        
        // assert
        
        Assert.NotNull(result);
        Assert.False(result.HelpRequested);
        Assert.False(result.VersionRequested);
        Assert.True(result.IsValid);
        Assert.False(result.HasMissingCommands);
        Assert.True(result.HasIgnoredCommands);
        var ignored = Assert.Single(result.Ignored);
        Assert.Equal(ignored.Name, "help");
        Assert.False(result.HasInvalidCommands);
        Assert.Null(result.Version);
        Assert.Null(result.HelpSection);
        Assert.Null(result.Get("help"));
    }
}