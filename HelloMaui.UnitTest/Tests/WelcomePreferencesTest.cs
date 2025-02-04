using HelloMaui.Services;
using HelloMaui.UnitTest.Mocks;

namespace HelloMaui.UnitTest.Tests;

public class WelcomePreferencesTest : BaseTest
{
    public override Task SetUp()
    {
        Services.ServiceProvider.GetService<IPreferences>()?.Clear();
        return base.SetUp();
    }

    public override Task TearDown()
    {
        Services.ServiceProvider.GetService<IPreferences>()?.Clear();
        return base.TearDown();
    }

    [Test]
    public void IsFirstRun_ByDefault_ReturnsTrue()
    {
        //Arrange
        var wp = new WelcomePreferences(Services.ServiceProvider.GetService<IPreferences>());
        //Act
        //Assert
        Assert.That(wp.IsFirstRun, Is.True);
    }

    [Test]
    public void IsFirstRun_ChangingValueToFalse_ReturnsFalse()
    {
        //Arrange
        var wp = new WelcomePreferences(Services.ServiceProvider.GetService<IPreferences>());
        //Act
        wp.IsFirstRun = false;
        //Assert
        Assert.That(wp.IsFirstRun, Is.False);
    }

    [Test]
    public void IsFirstRun_ChangingValueToFalseThenTrue_ReturnsTrue()
    {
        //Arrange
        var wp = new WelcomePreferences(Services.ServiceProvider.GetService<IPreferences>());
        //Act
        wp.IsFirstRun = false;
        wp.IsFirstRun = true;
        //Assert
        Assert.That(wp.IsFirstRun, Is.True);
    }
}