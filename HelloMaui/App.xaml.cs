using System.Diagnostics;

namespace HelloMaui;

public partial class App : Application
{
    private readonly AppShell _shell;

    public App(AppShell shell)
    {
        InitializeComponent();
        _shell = shell;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // return new Window(new AppShell());
        // return new Window(new MainPage());
        // return new Window(new MainPage_XAML());
        return new Window(_shell);
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