using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class CharOptionTests
{
    private static CharOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
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
    [InlineData("a")]
    [InlineData("1")]
    [InlineData("@")]
    [InlineData("-")]
    public void ValueIsValid_should_return_true_as_expected(string input)
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
    [InlineData("")]
    [InlineData("not-a-char")]
    public void ValueIsValid_should_return_false_if_value_is_not_char(string input)
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