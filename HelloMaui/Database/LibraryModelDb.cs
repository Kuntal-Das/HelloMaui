using System.Data.Common;
using HelloMaui.Models;

namespace HelloMaui.Database;

public class LibraryModelDb(IFileSystem fileSystem) : BaseDataBase(fileSystem)
{
    public Task<List<LibraryModel>> GetLibraryListAsync(CancellationToken ct = default)
    {
        return ExecuteAsync<List<LibraryModel>, LibraryModel>(
            dbcon => dbcon.Table<LibraryModel>().ToListAsync(), ct);
    }

    public Task<int> InsertLibraryModelAsync(IEnumerable<LibraryModel> libraryModels, CancellationToken ct = default)
    {
        return ExecuteAsync<int, LibraryModel>(dbcon => dbcon.InsertAllAsync(libraryModels), ct);
    }
}