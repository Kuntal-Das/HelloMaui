using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using HelloMaui.ViewModels;
using HelloMaui.Views;

namespace HelloMaui.Pages;

public class MainPage : BaseContentPage<MainViewModel>
{
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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.ShowPopup(new WelcomePopUp());
    }

    public MainPage(MainViewModel mainViewModel) : base(mainViewModel)
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
                            .Margins(bottom: 8)
                            .Center()
                            .TextCenter()
                            .Behaviors(new UserStoppedTypingBehavior()
                            {
                                BindingContext = mainViewModel,
                                StoppedTypingTimeThreshold = 1000,
                                ShouldDismissKeyboardAutomatically = true,
                            }.Bind(UserStoppedTypingBehavior.CommandProperty,
                                getter: (MainViewModel mvm) => mvm.UserStoppedTypingCommand))
                            .BindTapGesture(getter: (MainViewModel mvm) => mvm.SearchBarDoubleTappedCommand,
                                numberOfTapsRequired: 2)
                            .Bind(SearchBar.TextProperty, mode: BindingMode.TwoWay,
                                getter: (MainViewModel mvm) => mvm.SearchText,
                                setter: (mvm, newVal) => mvm.SearchText = newVal)
                            .Bind(SearchBar.IsEnabledProperty, (MainViewModel mvm) => mvm.IsSearchBarEnabled),
                        Footer = new Label()
                            {
                                Text = "Dotnet Maui: zero to hero"
                            }
                            .AppThemeBinding(Label.TextColorProperty, Color.FromArgb("#474f52"), Colors.DarkGray)
                            .Paddings(left: 8)
                            .FontSize(10)
                            .Center()
                            .TextCenter(),
                        SelectionMode = SelectionMode.Single,
                        ItemTemplate = new MauiLibrariesDataTemplate(),
                    }
                    .Bind(ItemsView.ItemsSourceProperty, (MainViewModel mvm) => mvm.MauiLibraries)
                    .Bind(SelectableItemsView.SelectionChangedCommandProperty,
                        (MainViewModel mvm) => mvm.SelectionChangedCommand)
                    .Bind(SelectableItemsView.SelectedItemProperty, (MainViewModel mvm) => mvm.SelectedLibraryItem,
                        (mvm, newVal) => mvm.SelectedLibraryItem = newVal),
            }
            .Bind(RefreshView.CommandProperty, (MainViewModel mvm) => mvm.RefreshCommand)
            .Bind(RefreshView.IsRefreshingProperty, (MainViewModel mvm) => mvm.IsRefreshing,
                (mvm, newVal) => mvm.IsRefreshing = newVal)
            .Margins(top: 24);
    }
}

internal sealed class WelcomePopUp : Popup
{
    public WelcomePopUp()
    {
        Content = new Label()
            .Text("WELCOME TO MAUI!")
            .Font(size: 42, bold: true)
            .Center()
            .TextCenter();
    }
}