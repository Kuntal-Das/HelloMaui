using HelloMaui.Services;
using HelloMaui.UnitTest.Mocks;

namespace HelloMaui.UnitTest.Tests;

public class WelcomePreferencesTest : BaseTest
{
    [Test]
    public void IsFirstRun_Default_True()
    {
        //Arrange
        var wp = new WelcomePreferences(new MockPreferences());
        //Act
        //Assert
        Assert.That(wp.IsFirstRun, Is.True);
    } 
}