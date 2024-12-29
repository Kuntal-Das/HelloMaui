using HelloMaui.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace HelloMaui.Pages;

public class BaseContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel
{
    protected BaseContentPage(TViewModel viewModel)
    {
        this.BindingContext = viewModel;
        On<iOS>().SetUseSafeArea(true);
    }
}