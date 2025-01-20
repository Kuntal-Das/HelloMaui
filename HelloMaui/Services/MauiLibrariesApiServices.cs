using HelloMaui.Models;
using Refit;

namespace HelloMaui.Services;

public class MauiLibrariesApiServices
{
    private readonly IMauiLibraries _client;

    public MauiLibrariesApiServices(IMauiLibraries client)
    {
        _client = client;
    }

    public Task<List<LibraryModel>> GetMauiLibrariesAsync()
    {
        return _client.GetMauiLibrariesAsync();
    }
}

//https://6dhbgfw1de.execute-api.us-west-1.amazonaws.com/default/MauiLibraries
public interface IMauiLibraries
{
    [Get("default/MauiLibraries")]
    Task<List<LibraryModel>> GetMauiLibrariesAsync();
}