using Simple.ArgumentParser.Options;

namespace Simple.ArgumentParser.Tests.Options;

public class OptionSettingsTests
{
    [Fact]
    public void Constructor_sets_default_values_to_false()
    {
        // arrange
        // act

        var subjectUnderTest = new OptionSettings();

        // assert
        
        Assert.False(subjectUnderTest.Required); 
        Assert.False(subjectUnderTest.Strict); 
    } 
}