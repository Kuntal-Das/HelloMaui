using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using HelloMaui.Pages;

namespace HelloMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        MauiLibraries = new(GetAllLibraries());
        SearchText = "";
        IsSearchBarEnabled = true;
    }

    private static IEnumerable<LibraryModel> GetAllLibraries()
    {
        yield return new LibraryModel()
        {
            Title = "Microsoft.Maui",
            Description =
                ".NET Multi-platform App UI is a framework for building native device applications spanning mobile, tablet, and desktop",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/microsoft.maui.controls/8.0.3/icon"
        };

        yield return new LibraryModel()
        {
            Title = "CommunityToolkit.Maui",
            Description =
                "The .NET MAUI Community Toolkit is a community-created library that contains .NET MAUI Extensions, Advanced UI/UX Controls, and Behaviors to help make your life as a .NET MAUI developer easier",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui/5.2.0/icon"
        };

        yield return new LibraryModel
        {
            Title = "CommunityToolkit.Maui.Markup",
            Description =
                "The .NET MAUI Markup Community Toolkit is a community-created library that contains Fluent C# Extension Methods to easily create your User Interface in C#",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui.markup/3.2.0/icon"
        };

        yield return new LibraryModel
        {
            Title = "CommunityToolkit.MVVM",
            Description =
                "This package includes a .NET MVVM library with helpers such as ObservableObject, ObservableRecipient, ObservableValidator, RelayCommand, AsyncRelayCommand, WeakReferenceMessenger, StrongReferenceMessenger and IoC",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.mvvm/8.2.0/icon"
        };

        yield return new LibraryModel
        {
            Title = "Sentry.Maui",
            Description =
                "Bad software is everywhere, and we're tired of it. Sentry is on a mission to help developers write better software faster, so we can get back to enjoying technology",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/sentry.maui/3.33.1/icon"
        };

        yield return new LibraryModel
        {
            Title = "Esri.ArcGISRuntime.Maui",
            Description =
                "Contains APIs and UI controls for building native mobile and desktop apps with the .NET Multi-platform App UI (.NET MAUI) cross-platform framework",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/esri.arcgisruntime.maui/100.14.1-preview3/icon"
        };

        yield return new LibraryModel
        {
            Title = "Syncfusion.Maui.Core",
            Description =
                "This package contains .NET MAUI Avatar View, .NET MAUI Badge View, .NET MAUI Busy Indicator, .NET MAUI Effects View, and .NET MAUI Text Input Layout components for .NET MAUI application",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/syncfusion.maui.core/21.2.10/icon"
        };

        yield return new LibraryModel
        {
            Title = "DotNet.Meteor",
            Description = "A VSCode extension that can run and debug .NET apps (based on Clancey VSCode.Comet)",
            ImageSource =
                "https://nromanov.gallerycdn.vsassets.io/extensions/nromanov/dotnet-meteor/3.0.3/1686392945636/Microsoft.VisualStudio.Services.Icons.Default"
        };
    }

    [ObservableProperty] private string? _searchText;
    [ObservableProperty] private bool _isSearchBarEnabled;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private object? _selectedLibraryItem;
    private readonly IDispatcher _dispatcher;
    public ObservableCollection<LibraryModel> MauiLibraries { get; }

    [RelayCommand]
    private
#if IOS
        async Task
#else
        void
#endif
        UserStoppedTyping()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
#if IOS
            await _dispatcher.DispatchAsync(() => MauiLibraries.Clear()).ConfigureAwait(false);
#else
            MauiLibraries.Clear();
#endif
            foreach (var library in GetAllLibraries())
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
            for (var i = 0; i < MauiLibraries.Count; i++)
            {
                if (!MauiLibraries[i].Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                {
#if IOS
                    await _dispatcher.DispatchAsync(() => MauiLibraries.RemoveAt(i)).ConfigureAwait(false);
#else
                    MauiLibraries.RemoveAt(i);
#endif
                }
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
        try
        {
            IsSearchBarEnabled = false;
            await Task.Delay(2000);
            var newLib = new LibraryModel()
            {
                Title = "SharpNado.Tabs",
                Description =
                    "Pure MAUI and Xamarin.Forms, including fixed tabs, scrollable tabs, bottom tabs, badge, segmented tabs etc.",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/sharpnado.tabs/3.0.0/icon"
            };
#if IOS
            await _dispatcher.DispatchAsync(() => MauiLibraries.Add(newLib));
#else
            MauiLibraries.Add(newLib);
#endif
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
        finally
        {
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