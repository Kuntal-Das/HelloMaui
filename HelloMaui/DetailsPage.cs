using CommunityToolkit.Maui.Markup;

namespace HelloMaui;

public class DetailsPage : BaseContentPage, IQueryAttributable
{
    private readonly string _libModelKey = nameof(LibraryModel);
    private readonly Image _image;
    private readonly Label _titleLabel;
    private readonly Label _descriptionLabel;

    public DetailsPage()
    {
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
#if ANDROID
            TextOverride = "⬅ List",
#else
            TextOverride = "List"
#endif
        });
        Title = "Details Page";
        Content = new VerticalStackLayout()
        {
            Spacing = 12,
            Children =
            {
                new Image()
                    .Center()
                    .Size(250)
                    .Margins(bottom: 12)
                    .Aspect(Aspect.AspectFit)
                    .Assign(out _image),
                new Label()
                    .Font(italic: true, size: 16)
                    .Center()
                    .TextCenter()
                    .Assign(out _descriptionLabel),
                new Button()
                    .Text("Back")
                    .Invoke(btn => btn.Clicked += BackBtnClicked)
            }
        }.Padding(12);
    }

    private async void BackBtnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..", animate: true);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue(_libModelKey, out var tLibModel) || tLibModel is not LibraryModel libModel) return;
        _image.Source = libModel.ImageSource;
        Title = libModel.Title;
        // _titleLabel.Text = libModel.Title;
        _descriptionLabel.Text = libModel.Description;
    }
}