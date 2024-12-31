using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class MainPage : BaseContentPage<MainViewModel>
{
    public MainPage(MainViewModel mainViewModel) : base(mainViewModel)
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.ShowPopup(new WelcomePopUp());
    }
}