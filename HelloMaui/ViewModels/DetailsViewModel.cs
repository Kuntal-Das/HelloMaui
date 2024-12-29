using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;

namespace HelloMaui.ViewModels;

public class DetailsViewModel : BaseViewModel, IQueryAttributable
{
    public DetailsViewModel()
    {
        BackCommand = new AsyncRelayCommand(BackBtnClicked);
    }

    public static readonly string LibModelKey = nameof(LibraryModel);

    private ImageSource _imageSource;
    private string _title;
    private string _description;
    public ICommand BackCommand { get; }


    public ImageSource ImageSource
    {
        get => _imageSource;
        set => SetProperty(ref _imageSource, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }


    private async Task BackBtnClicked()
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