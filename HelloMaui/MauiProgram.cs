using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using CustomControl.Handlers;
using CustomControl.View;
using HelloMaui.Database;
using HelloMaui.Pages;
using HelloMaui.Services;
using HelloMaui.ViewModels;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Polly;
using Refit;

namespace HelloMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureMauiHandlers(handlers => { handlers.AddHandler<CalendarView, CalendarHandler>(); })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IPreferences>(Preferences.Default);
        builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
        builder.Services.AddSingleton<LibraryModelDb>();
        builder.Services.AddSingleton<WelcomePreferences>();

        builder.Services.AddLibrariesGaphqlClient()
            .ConfigureHttpClient(
                static client => client.BaseAddress =
                    new Uri("https://t41fbiwwda.execute-api.us-west-1.amazonaws.com/graphql"),
                static builder =>
                    builder.AddStandardResilienceHandler(options =>
                        options.Retry = new MobileHttpRetryStategyOptions()));

        builder.Services.AddRefitClient<IMauiLibraries>().ConfigureHttpClient(client =>
                client.BaseAddress = new Uri("https://6dhbgfw1de.execute-api.us-west-1.amazonaws.com/"))
            .AddStandardResilienceHandler(options => options.Retry = new MobileHttpRetryStategyOptions());

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<CalendarPage>();

        return builder.Build();
    }

    sealed class MobileHttpRetryStategyOptions : HttpRetryStrategyOptions
    {
        public MobileHttpRetryStategyOptions()
        {
            BackoffType = DelayBackoffType.Exponential;
            MaxRetryAttempts = 3;
            UseJitter = true;
            Delay = TimeSpan.FromSeconds(2);
        }
    }
}