namespace HelloMaui.Services;

public class WelcomePreferences
{
    private readonly IPreferences _preferences;

    public WelcomePreferences(IPreferences preferences)
    {
        _preferences = preferences;
    }
    public bool IsFirstRun
    {
        get => _preferences.Get(nameof(IsFirstRun), true);
        set => _preferences.Set(nameof(IsFirstRun), value);
    }
}