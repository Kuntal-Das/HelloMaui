using HelloMaui.Pages;

namespace HelloMaui;

public class AppShell : Shell
{
    public AppShell(MainPage mainPageCSharp)
    {
        Items.Add(mainPageCSharp);
        CreateRoutes();
    }

    public static string GetRoute<T>() where T : ContentPage
    {
        if (typeof(T) == typeof(DetailsPage))
        {
            return $"//{nameof(MainPage)}//{nameof(DetailsPage)}";
        }

        if (typeof(T) == typeof(MainPage))
        {
            return $"//{nameof(MainPage)}";
        }

        throw new NotImplementedException($"{typeof(T)} is not configured in Routing");
    }

    private static void CreateRoutes()
    {
        Routing.RegisterRoute(GetRoute<MainPage>(), typeof(MainPage));
        Routing.RegisterRoute(GetRoute<DetailsPage>(), typeof(DetailsPage));
    }
}