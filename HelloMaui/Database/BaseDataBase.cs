using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Retry;
using SQLite;

namespace HelloMaui.Database;

internal abstract class BaseDataBase
{
    private readonly Lazy<SQLiteAsyncConnection> _databaseHolder;

    protected BaseDataBase(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "database.db3");
        _databaseHolder = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(dbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));
    }

    SQLiteAsyncConnection Database => _databaseHolder.Value;

    protected async Task<TReturn> ExecuteAsync<TReturn, TDatabase>(Func<SQLiteAsyncConnection, Task<TReturn>> action,
        CancellationToken cancellationToken = default, int maxRetries = 10)
    {
        var dbCon = await GetConnectionAsync<TDatabase>().ConfigureAwait(false);

        var resilencePiperline = new ResiliencePipelineBuilder<TReturn>()
            .AddRetry(new RetryStrategyOptions<TReturn>()
            {
                MaxRetryAttempts = maxRetries,
                Delay = TimeSpan.FromMilliseconds(5),
                BackoffType = DelayBackoffType.Exponential
            }).Build();

        return await resilencePiperline.ExecuteAsync(
            async cts => await action.Invoke(Database).ConfigureAwait(false),
            cancellationToken);
    }

    async ValueTask<SQLiteAsyncConnection> GetConnectionAsync<T>()
    {
        if (!Database.TableMappings.Any(static x => x.MappedType == typeof(T)))
        {
            await Database.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
            await Database.CreateTableAsync(typeof(T)).ConfigureAwait(false);
        }

        return Database;
    }
}