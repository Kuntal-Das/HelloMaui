using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using HelloMaui.Pages;
using HelloMaui.Services;

namespace HelloMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IDispatcher dispatcher, IMauiLibraries mauiApiServices)
    {
        _dispatcher = dispatcher;
        _mauiApiServices = mauiApiServices;
        MauiLibraries = new();
        SearchText = "";
        IsSearchBarEnabled = true;
    }

    public IReadOnlyList<LibraryModel> _mauiLibrariesCache;
    [ObservableProperty] private string? _searchText;
    [ObservableProperty] private bool _isSearchBarEnabled;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private object? _selectedLibraryItem;
    private readonly IDispatcher _dispatcher;
    private readonly IMauiLibraries _mauiApiServices;

    public ObservableCollection<LibraryModel> MauiLibraries { get; }

    [RelayCommand]
    private async Task UserStoppedTyping()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
#if IOS
            await _dispatcher.DispatchAsync(() => MauiLibraries.Clear()).ConfigureAwait(false);
#else
            MauiLibraries.Clear();
#endif
            var libs = _mauiLibrariesCache ?? Array.Empty<LibraryModel>();
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
        try
        {
            IsSearchBarEnabled = false;
            _mauiLibrariesCache ??= await _mauiApiServices.GetMauiLibrariesAsync().ConfigureAwait(false);

            foreach (var library in _mauiLibrariesCache)
            {
#if IOS
                await _dispatcher.DispatchAsync(() => MauiLibraries.Add(library));
#else
                MauiLibraries.Add(library);
#endif
            }
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
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