using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class OptionTypeTests
{
    [Fact]
    public void Constructor_sets_default_to_alpha()
    {
        // arrange
        // act

        OptionType subjectUnderTest = default;
        
        // assert
        
        Assert.Equal(subjectUnderTest, OptionType.Alpha);
    }
}