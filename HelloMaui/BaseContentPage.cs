using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace HelloMaui;

public class BaseContentPage : ContentPage
{
    public BaseContentPage()
    {
        On<iOS>().SetUseSafeArea(true);
        // On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
    }
}