using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class DoubleOptionTests
{
    private static DoubleOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
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
    [InlineData("123")]
    [InlineData("1.23")]
    [InlineData("1,23")]
    [InlineData("0")]
    [InlineData("-0")]
    public void ValueIsValid_should_return_true_if_input_is_double(string input)
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
    [InlineData("not-a-double")]
    [InlineData("")]
    public void ValueIsValid_should_return_false_if_input_is_not_double(string input)
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