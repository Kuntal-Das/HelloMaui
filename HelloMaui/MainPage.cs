using System.Collections;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup;

namespace HelloMaui;

public class MainPage : BaseContentPage
{
    private readonly ObservableCollection<LibraryModel> _mauiLibraries =
        new ObservableCollection<LibraryModel>(GetAllLibraries());

    private readonly SearchBar _searchBar;

    internal enum Row
    {
        Image,
        Label,
        Entry,
        LargeTextLabel
    }

    internal enum Column
    {
        Entry1,
        Entry2,
        Entry3
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

    public MainPage()
    {
        Title = "Maui Collection View";
        this.AppThemeColorBinding(BackgroundColorProperty, Colors.LightBlue, Color.FromArgb("#3b4a4f"));
        Content = new RefreshView()
            {
                Content = new CollectionView()
                {
                    Header = new SearchBar()
                        .Placeholder("Search Titles")
                        .AppThemeBinding(Label.TextColorProperty, Colors.Black, Colors.LightGray)
                        .Center()
                        .TextCenter()
                        .Behaviors(new UserStoppedTypingBehavior()
                        {
                            StoppedTypingTimeThreshold = 1000,
                            ShouldDismissKeyboardAutomatically = true,
                            Command = new Command(UserStoppedTyping)
                        })
                        .TapGesture(SearchBarDoubleTapped, 2)
                        .Assign(out _searchBar),
                    Footer = new Label() { Text = "Dotnet Maui: zero to hero" }
                        .AppThemeBinding(Label.TextColorProperty, Color.FromArgb("#474f52"), Colors.DarkGray)
                        .Paddings(left: 8)
                        .FontSize(10)
                        .Center()
                        .TextCenter(),
                    SelectionMode = SelectionMode.Single,
                    ItemsSource = _mauiLibraries,
                    ItemTemplate = new MauiLibrariesDataTemplate(),
                }.Invoke(cv => cv.SelectionChanged += SelectionChanged),
            }.Invoke((rv => rv.Refreshing += HandelRefreshing))
            .Margins(top: 24);
    }

    private async void SearchBarDoubleTapped()
    {
        try
        {
            await Toast.Make(".Net MAUI rules!").Show();
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }

    private void UserStoppedTyping(object obj)
    {
        if (obj is string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _mauiLibraries.Clear();
                foreach (var library in GetAllLibraries())
                {
                    _mauiLibraries.Add(library);
                }
            }
            else
            {
                for (var i = 0; i < _mauiLibraries.Count; i++)
                {
                    if (!_mauiLibraries[i].Title.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                        _mauiLibraries.RemoveAt(i);
                }
            }
        }
    }

    private async void HandelRefreshing(object? sender, EventArgs e)
    {
        try
        {
            _searchBar.IsEnabled = false;
            ArgumentNullException.ThrowIfNull(sender);
            var rv = (RefreshView)sender;
            await Task.Delay(2000);

            _mauiLibraries.Add(new()
            {
                Title = "SharpNado.Tabs",
                Description =
                    "Pure MAUI and Xamarin.Forms, including fixed tabs, scrollable tabs, bottom tabs, badge, segmented tabs etc.",
                ImageSource = "https://api.nuget.org/v3-flatcontainer/sharpnado.tabs/3.0.0/icon"
            });

            rv.IsRefreshing = false;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
        finally
        {
            _searchBar.IsEnabled = true;
        }
    }

    private static async void SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(sender);

            if (e.CurrentSelection.FirstOrDefault() is not LibraryModel lm) return;
            await Toast.Make($"{lm.Title} Tapped").Show();
            await Task.Delay(500);
            if (sender is CollectionView collectionView)
                collectionView.SelectedItem = null;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message).Show();
        }
    }
}