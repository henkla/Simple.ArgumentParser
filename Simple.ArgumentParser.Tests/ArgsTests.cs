namespace Simple.ArgumentParser.Tests;

public class ArgsTests
{
    [Fact]
    public void IsValid_should_return_true_if_no_missing_or_invalid_arguments()
    {
        // arrange
        // act

        var args = new Args();

        // assert

        Assert.True(args.IsValid);
    }
    
    [Fact]
    public void IsValid_should_return_false_if_any_missing_argument_exists()
    {
        // arrange
        // act

        var args = new Args();
        args.Missing.Add("missing-argument");

        // assert

        Assert.False(args.IsValid);
    }
    
    [Fact]
    public void IsValid_should_return_false_if_any_invalid_argument_exists()
    {
        // arrange
        // act

        var args = new Args();
        args.Invalid.Add("invalid-argument");

        // assert

        Assert.False(args.IsValid);
    }
    
    [Fact]
    public void HasIgnoredCommands_should_return_true_if_any_ignored_commands_exists()
    {
        // arrange
        // act

        var args = new Args();
        args.Ignored.Add(new Command { Name = "ignored-command" });

        // assert

        Assert.True(args.HasIgnoredCommands);
    }
    
    [Fact]
    public void HasIgnoredCommands_should_return_false_if_no_ignored_commands_exists()
    {
        // arrange
        // act

        var args = new Args();

        // assert

        Assert.False(args.HasIgnoredCommands);
    }
    
    [Fact]
    public void HasMissingCommands_should_return_true_if_any_missing_commands_exists()
    {
        // arrange
        // act

        var args = new Args();
        args.Missing.Add("missing-command");

        // assert

        Assert.True(args.HasMissingCommands);
    }
    
    [Fact]
    public void HasMissingCommands_should_return_false_if_no_missing_commands_exists()
    {
        // arrange
        // act

        var args = new Args();

        // assert

        Assert.False(args.HasMissingCommands);
    }
    
    [Fact]
    public void HasInvalidCommands_should_return_true_if_any_invalid_commands_exists()
    {
        // arrange
        // act

        var args = new Args();
        args.Invalid.Add("invalid-command");

        // assert

        Assert.True(args.HasInvalidCommands);
    }
    
    [Fact]
    public void HasInvalidCommands_should_return_false_if_no_invalid_commands_exists()
    {
        // arrange
        // act

        var args = new Args();

        // assert

        Assert.False(args.HasInvalidCommands);
    }

    [Fact]
    public void HelpRequested_should_return_true_if_help_flag_is_specified()
    {
        // arrange
        // act

        var args = new ArgumentParser().AddHelpOption().Parse(["--help"]);
        
        // assert
        
        Assert.True(args.HelpRequested);
    }
    
    [Fact]
    public void HelpRequested_should_return_false_if_help_flag_specified()
    {
        // arrange
        // act

        var args = new ArgumentParser().AddHelpOption().Parse(["--not-help"]);
        
        // assert
        
        Assert.False(args.HelpRequested);
    }
    
    [Fact]
    public void VersionRequested_should_return_true_if_version_flag_is_specified()
    {
        // arrange
        // act

        var args = new ArgumentParser().AddVersionOption().Parse(["--version"]);
        
        // assert
        
        Assert.True(args.VersionRequested);
    }
    
    [Fact]
    public void HelpRequested_should_return_false_if_version_flag_is_specified()
    {
        // arrange
        // act

        var args = new ArgumentParser().AddVersionOption().Parse(["--not-version"]);
        
        // assert
        
        Assert.False(args.VersionRequested);
    }

    [Fact]
    public void HelpSection_is_not_empty_if_help_flag_is_specified()
    {
        // arrange
        // act
        
        var args = new ArgumentParser().AddHelpOption().Parse(["--help"]);
        
        // assert
        
        Assert.NotNull(args.HelpSection);
        Assert.NotEmpty(args.HelpSection);
    }
    
    [Fact]
    public void HelpSection_is_empty_if_help_flag_is_not_specified()
    {
        // arrange
        // act
        
        var args = new ArgumentParser().AddHelpOption().Parse(["--not-help"]);
        
        // assert
        
        Assert.Null(args.HelpSection);
    }
    
    [Fact]
    public void Version_is_not_empty_if_version_flag_is_specified()
    {
        // arrange
        // act
        
        var args = new ArgumentParser().AddVersionOption().Parse(["--version"]);
        
        // assert
        
        Assert.NotNull(args.Version);
        Assert.NotEmpty(args.Version);
    }
    
    [Fact]
    public void Version_is_empty_if_version_flag_is_not_specified()
    {
        // arrange
        // act
        
        var args = new ArgumentParser().AddVersionOption().Parse(["--not-version"]);
        
        // assert
        
        Assert.Null(args.Version);
    }
    
    [Fact]
    public void Any_returns_true_if_any_valid_command_exists()
    {
        // arrange
        // act
        
        var args = new Args { Valid = { new Command { Name = "valid-command" } } };
        
        // assert
        
        Assert.True(args.Any());
    }
    
    [Fact]
    public void Any_returns_false_if_no_valid_command_exists()
    {
        // arrange
        // act

        var args = new Args();
        
        // assert
        
        Assert.False(args.Any());
    }
    
    [Fact]
    public void Get_returns_matching_command_if_valid_key()
    {
        // arrange

        var args = new Args { Valid = { new Command { Name = "valid-command" } } };
        
        // act
        
        var result = args.Get("valid-command");
        
        // assert
        
        Assert.NotNull(result);
        Assert.Equal(result.Name, "valid-command");
    }
    
    [Fact]
    public void Get_returns_null_if_invalid_key()
    {
        // arrange

        var args = new Args { Valid = { new Command { Name = "valid-command" } } };
        
        // act
        
        var result = args.Get("invalid-key");
        
        // assert
        
        Assert.Null(result);
    }
    
    [Fact]
    public void GetAll_returns_all_valid_commands()
    {
        // arrange

        var args = new Args { Valid =
        {
            new Command { Name = "valid-command-1" },
            new Command { Name = "valid-command-2" },
            new Command { Name = "valid-command-3" }
        } };
        
        // act

        var result = args.GetAll();
        
        // assert
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count, 3);
    }
}