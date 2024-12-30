using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;

namespace HelloMaui.ViewModels;

public partial class DetailsViewModel : ObservableObject, IQueryAttributable
{
    public static readonly string LibModelKey = nameof(LibraryModel);
    [ObservableProperty] private ImageSource _imageSource;
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..", animate: true);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue(LibModelKey, out var tLibModel)
            || tLibModel is not LibraryModel libModel) return;
        Title = libModel.Title;
        ImageSource = libModel.ImageSource;
        Description = libModel.Description;
    }
}