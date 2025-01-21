using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using HelloMaui.Models;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class MainPage : BaseContentPage<MainViewModel>
{
    private readonly IPreferences _preferences;

    public MainPage(MainViewModel mainViewModel, IPreferences preferences) : base(mainViewModel)
    {
        InitializeComponent();
        _preferences = preferences;
    }

    public bool IsFirstRun
    {
        get => _preferences.Get(nameof(IsFirstRun), true);
        set => _preferences.Set(nameof(IsFirstRun), value);
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

        if (IsFirstRun)
        {
            this.ShowPopup(new WelcomePopUp());
            IsFirstRun = false;
        }
    }
}