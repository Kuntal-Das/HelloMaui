using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using HelloMaui.Models;
using HelloMaui.Services;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class MainPage : BaseContentPage<MainViewModel>
{
    private readonly WelcomePreferences _welcomePreferences;

    public MainPage(MainViewModel mainViewModel, WelcomePreferences welcomePreferences) : base(mainViewModel)
    {
        InitializeComponent();
        _welcomePreferences = welcomePreferences;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // && collectionView.ItemsSource is ObservableCollection<LibraryModel> collection
        if (main_rv.Children[0] is CollectionView { ItemsSource: ObservableCollection<LibraryModel> collection } &&
            !collection.Any())
        {
            main_rv.IsRefreshing = true;
        }

        if (_welcomePreferences.IsFirstRun)
        {
            this.ShowPopup(new WelcomePopUp());
            _welcomePreferences.IsFirstRun = false;
        }
    }
}