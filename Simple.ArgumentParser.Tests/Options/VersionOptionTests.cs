using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class VersionOptionTests
{
    private static VersionOption GetSubjectUnderTest() => new();
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    [InlineData(" ")]
    [InlineData("_")]
    [InlineData("abc")]
    [InlineData("bool")]
    [InlineData("--")]
    public void ValueIsValid_should_return_true_whatever_value_provided(string input)
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
}