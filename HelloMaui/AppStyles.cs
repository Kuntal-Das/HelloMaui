namespace HelloMaui;

public static class AppStyles
{
    public static T? GetResource<T>(string resourceName)
    {
        if (Application.Current != null && Application.Current.Resources.TryGetValue(resourceName, out object resource))
            return (T)resource;

        throw new KeyNotFoundException($"No resource named '{resourceName}' was found.");
    }
}