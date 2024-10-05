using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class AlphaOptionTests
{
    private static AlphaOption GetSubjectUnderTest(OptionSettings? settings = null) => new (
        name: "test-name",
        shortName: 't',
        description: "test-description",
        settings: settings ?? new OptionSettings());

    [Theory]
    [InlineData("alpha")]
    [InlineData("123")]
    [InlineData("true")]
    [InlineData(" ")]
    public void ValueIsValid_should_return_true(string inlineData)
    {
        // arrange

        var subjectUnderTest = GetSubjectUnderTest();

        // act

        var result = subjectUnderTest.ValueIsValid(inlineData, out var message);

        // assert

        Assert.True(result);
        Assert.Empty(message);
    }
}