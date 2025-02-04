namespace HelloMaui.UnitTest.Tests;

public abstract class BaseTest
{
    [TearDown]
    public virtual Task TearDown()
    {
        return Task.CompletedTask;
    }
    [SetUp]
    public virtual Task SetUp()
    {
        return Task.CompletedTask;
    }
}