using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Database;
using HelloMaui.Models;
using HelloMaui.Pages;
using HelloMaui.Services;

namespace HelloMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IDispatcher dispatcher, IMauiLibraries mauiApiServices, LibraryModelDb libraryModelDb,
        LibrariesGaphqlClient graphqlClient)
    {
        _dispatcher = dispatcher;
        _mauiApiServices = mauiApiServices;
        _libraryModelDb = libraryModelDb;
        _graphqlClient = new MauiLibraryGarphQlService(graphqlClient);
        MauiLibraries = [];
        SearchText = "";
        IsSearchBarEnabled = true;
    }

    [ObservableProperty] private string? _searchText;
    [ObservableProperty] private bool _isSearchBarEnabled;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private object? _selectedLibraryItem;
    private readonly IDispatcher _dispatcher;
    private readonly IMauiLibraries _mauiApiServices;
    private readonly LibraryModelDb _libraryModelDb;
    private readonly MauiLibraryGarphQlService _graphqlClient;

    public ObservableCollection<LibraryModel> MauiLibraries { get; }

    [RelayCommand]
    private async Task UserStoppedTyping(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
#if IOS
            await _dispatcher.DispatchAsync(() => MauiLibraries.Clear()).ConfigureAwait(false);
#else
            MauiLibraries.Clear();
#endif

            var libs = await _libraryModelDb.GetLibraryListAsync(ct).ConfigureAwait(false);

            foreach (var library in libs)
            {
#if IOS
                await _dispatcher.DispatchAsync(() => MauiLibraries.Add(library)).ConfigureAwait(false);
#else
                MauiLibraries.Add(library);
#endif
            }
        }
        else
        {
            var removeAts = new HashSet<int>();
            for (var i = 0; i < MauiLibraries.Count; i++)
            {
                if (!MauiLibraries[i].Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    removeAts.Add(i);
                }
            }

            foreach (var i in removeAts)
            {
#if IOS
                await _dispatcher.DispatchAsync(() => MauiLibraries.RemoveAt(i)).ConfigureAwait(false);
#else
                MauiLibraries.RemoveAt(i);
#endif
            }
        }
    }

    [RelayCommand]
    private async Task SearchBarDoubleTapped()
    {
        await Toast.Make(".Net MAUI rules!").Show();
    }

    [RelayCommand]
    private async Task SelectionChanged()
    {
        try
        {
            if (SelectedLibraryItem is not { } lm) return;
            await Shell.Current.GoToAsync(AppShell.GetRoute<DetailsPage>(), new Dictionary<string, object>()
            {
                { DetailsViewModel.LibModelKey, lm }
            });
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        var minWaitTask = Task.Delay(1500).ConfigureAwait(false);
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(550));
        try
        {
            IsSearchBarEnabled = false;
            var libs = await _libraryModelDb.GetLibraryListAsync(cts.Token).ConfigureAwait(false);
            if (libs.Count == 0)
            {
                // libs = await _mauiApiServices.GetMauiLibrariesAsync(cts.Token).ConfigureAwait(false);
                await foreach (var lib in _graphqlClient.GetLibrariesAsync(cts.Token).ConfigureAwait(false))
                {
#if IOS
                    await _dispatcher.DispatchAsync(() => MauiLibraries.Add(lib));
#else
                    MauiLibraries.Add(lib);
#endif
                }

                await _libraryModelDb.InsertLibraryModelAsync(MauiLibraries, cts.Token).ConfigureAwait(false);
            }
            else
            {
                foreach (var library in libs)
                {
#if IOS
                    await _dispatcher.DispatchAsync(() => MauiLibraries.Add(library));
#else
                    MauiLibraries.Add(library);
#endif
                }
            }
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show(cts.Token);
        }
        finally
        {
            await minWaitTask;
            IsRefreshing = false;
            IsSearchBarEnabled = true;
        }
    }

    [RelayCommand]
    private async Task GotoCalendarpage()
    {
        await Shell.Current.GoToAsync(AppShell.GetRoute<CalendarPage>());
    }
}