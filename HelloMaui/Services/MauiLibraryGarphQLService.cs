using System.Runtime.CompilerServices;
using HelloMaui.Models;
using StrawberryShake;

namespace HelloMaui.Services;

public class MauiLibraryGarphQlService(LibrariesGaphqlClient client)
{
    public async IAsyncEnumerable<LibraryModel> GetLibrariesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var response = await client.LibrariesQuery.ExecuteAsync(cancellationToken).ConfigureAwait(false);
        response.EnsureNoErrors();
        if (response.Data?.Libraries == null)
            throw new Exception("No libraries found");

        foreach (var lib in response.Data.Libraries)
        {
            yield return new LibraryModel()
            {
                Title = lib.Title,
                Description = lib.Description,
                ImageSource = lib.ImageSource,
            };
        }
    }
}