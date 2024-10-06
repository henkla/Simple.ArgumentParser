using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class FlagOptionTests
{
    private static FlagOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
        name: "test-name",
        shortName: 'c',
        description: "test-description",
        settings: settings ?? new OptionSettings());

    private static OptionSettings GetStrictSettings() => new OptionSettings
    {
        Strict = true,
        Required = false
    };
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ValueIsValid_should_return_true_if_input_is_none(string input)
    {
        // arrange
        
        var subjectUnderTest = GetSubjectUnderTest();
        
        // act

        var result = subjectUnderTest.ValueIsValid(input, out var message);
        
        // assert

        Assert.True(result);
        Assert.NotNull(message);
        Assert.Empty(message);
    }
    
    [Theory]
    [InlineData("1")]
    [InlineData("abc")]
    [InlineData("_")]
    public void ValueIsValid_should_return_false_if_any_value_at_all(string input)
    {
        // arrange
        
        var subjectUnderTest = GetSubjectUnderTest();
        
        // act

        var result = subjectUnderTest.ValueIsValid(input, out var message);
        
        // assert

        Assert.False(result);
        Assert.NotNull(message);
        Assert.NotEmpty(message);
    }
}