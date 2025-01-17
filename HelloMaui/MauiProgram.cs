﻿using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using CustomControl.Handlers;
using CustomControl.View;
using HelloMaui.Pages;
using HelloMaui.ViewModels;
using Microsoft.Extensions.Logging;

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

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<CalendarPage>();

        return builder.Build();
    }
}