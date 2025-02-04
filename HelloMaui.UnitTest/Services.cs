using HelloMaui.Pages;
using HelloMaui.UnitTest.Mocks;
using HelloMaui.ViewModels;

namespace HelloMaui.UnitTest;

internal class Services
{
    private static readonly Lazy<IServiceProvider> LazyProviderHolder = new Lazy<IServiceProvider>(CreateCollection);
    public static IServiceProvider ServiceProvider => LazyProviderHolder.Value;

    private static IServiceProvider CreateCollection()
    {
        var container = new ServiceCollection();

        //services
        container.AddSingleton<IPreferences, MockPreferences>();
        
        container.AddTransient<DetailsViewModel>();
        container.AddTransient<MainViewModel>();
        //viewmodels

        return container.BuildServiceProvider();
    }
}