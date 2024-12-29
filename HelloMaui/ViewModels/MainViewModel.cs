using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using HelloMaui.Pages;

namespace HelloMaui.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        SearchText = "";
        _isSearchBarEnabled = true;
        MauiLibraries = new(GetAllLibraries());
        UserStoppedTypingCommand = new RelayCommand(UserStoppedTyping);
        SearchBarDoubleTappedCommand = new AsyncRelayCommand(SearchBarDoubleTapped);
        SelectionChangedCommand = new AsyncRelayCommand(SelectionChanged);
        RefreshCommand = new AsyncRelayCommand(HandelRefreshing);
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

    private string? _searchText;
    private bool _isSearchBarEnabled;
    private bool _isRefreshing;
    private object _selectedLibraryItem;

    // public ObservableCollection<object> SelectedLibraries { get; }
    public ObservableCollection<LibraryModel> MauiLibraries { get; }
    public ICommand UserStoppedTypingCommand { get; }
    public ICommand SearchBarDoubleTappedCommand { get; }
    public ICommand SelectionChangedCommand { get; set; }
    public ICommand RefreshCommand { get; set; }

    public string? SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public bool IsSearchBarEnabled
    {
        get => _isSearchBarEnabled;
        set => SetProperty(ref _isSearchBarEnabled, value);
    }

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }

    public object SelectedLibraryItem
    {
        get => _selectedLibraryItem;
        set => SetProperty(ref _selectedLibraryItem, value);
    }

    private void UserStoppedTyping()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            MauiLibraries.Clear();
            foreach (var library in GetAllLibraries())
            {
                MauiLibraries.Add(library);
            }
        }
        else
        {
            for (var i = 0; i < MauiLibraries.Count; i++)
            {
                if (!MauiLibraries[i].Title.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
                    MauiLibraries.RemoveAt(i);
            }
        }
    }

    private async Task SearchBarDoubleTapped()
    {
        await Toast.Make(".Net MAUI rules!").Show();
    }

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

    private async Task HandelRefreshing()
    {
        try
        {
            IsSearchBarEnabled = false;
            await Task.Delay(2000);

            MauiLibraries.Add(new()
            {
                Title = "SharpNado.Tabs",
                Description =
                    "Pure MAUI and Xamarin.Forms, including fixed tabs, scrollable tabs, bottom tabs, badge, segmented tabs etc.",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/sharpnado.tabs/3.0.0/icon"
            });
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
}