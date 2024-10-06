using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class IntegerOptionTests
{
    private static IntegerOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
        name: "test-name",
        shortName: 't',
        description: "test-description",
        settings: settings ?? new OptionSettings());

    private static OptionSettings GetStrictSettings() => new OptionSettings
    {
        Strict = true,
        Required = false
    };

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    [InlineData("-0")]
    [InlineData("1")]
    public void ValueIsValid_should_return_true_if_input_is_integer(string input)
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
    [InlineData("3.14")]
    [InlineData("abc")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("3,14")]
    [InlineData("five")]
    public void ValueIsValid_should_return_false_if_input_is_not_integer(string input)
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