using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class BooleanOptionTests
{
    private static BooleanOption GetSubjectUnderTest(OptionSettings? settings = null) => new(
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
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("1")]
    [InlineData("0")]
    public void ValueIsValid_should_return_true_with_strict_off(string inlineData)
    {
        // arrange

        var subjectUnderTest = GetSubjectUnderTest();

        // act

        var result = subjectUnderTest.ValueIsValid(inlineData, out var message);

        // assert

        Assert.True(result);
        Assert.Empty(message);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("0")]
    public void ValueIsValid_should_return_false_with_strict_on(string inlineData)
    {
        // arrange

        var subjectUnderTest = GetSubjectUnderTest(GetStrictSettings());

        // act

        var result = subjectUnderTest.ValueIsValid(inlineData, out var message);

        // assert

        Assert.False(result);
        Assert.NotEmpty(message);
    }
    
    [Theory]
    [InlineData("-1")]
    [InlineData("-0")]
    [InlineData("text")]
    public void ValueIsValid_should_return_false_with_strict_off(string inlineData)
    {
        // arrange

        var subjectUnderTest = GetSubjectUnderTest();

        // act

        var result = subjectUnderTest.ValueIsValid(inlineData, out var message);

        // assert

        Assert.False(result);
        Assert.NotEmpty(message);
    }
}