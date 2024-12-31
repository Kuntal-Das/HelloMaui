using System.Diagnostics;

namespace HelloMaui;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // return new Window(new AppShell());
        // return new Window(new MainPage());
        // return new Window(new MainPage_XAML());

        // return new Window(Handler.MauiContext.Services.GetService<AppShell>());
        // return new Window(_appShell);
        //TODO: app restart casuses 'MauiContext is null.'
        return new Window(_serviceProvider.GetService<AppShell>());
    }

    protected override void OnResume()
    {
        base.OnResume();
        Trace.WriteLine("*** App Resumed ***");
    }

    protected override void OnSleep()
    {
        base.OnSleep();
        Trace.WriteLine("*** App Sleep ***");
    }

    protected override void OnStart()
    {
        base.OnStart();
        Trace.WriteLine("*** App Started ***");
    }
}