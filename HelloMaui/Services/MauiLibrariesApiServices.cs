using HelloMaui.Models;
using Refit;

namespace HelloMaui.Services;
//https://6dhbgfw1de.execute-api.us-west-1.amazonaws.com/default/MauiLibraries
public interface IMauiLibraries
{
    [Get("/default/MauiLibraries")]
    Task<List<LibraryModel>> GetMauiLibrariesAsync(CancellationToken cancellationToken = default);
}