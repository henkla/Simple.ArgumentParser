using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class EnumerateOptionTests
{
    private static EnumerateOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
        name: "test-name",
        shortName: 'c',
        description: "test-description",
        [ "accepted1", "accepted2" ],
        settings: settings ?? new OptionSettings());

    private static OptionSettings GetStrictSettings() => new OptionSettings
    {
        Strict = true,
        Required = false
    };
    
    [Theory]
    [InlineData("accepted1")]
    [InlineData("accepted2")]
    public void ValueIsValid_should_return_true_if_input_is_defined(string input)
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
    [InlineData("accepted3")]
    [InlineData("")]
    public void ValueIsValid_should_return_false_if_input_is_not_defined(string input)
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