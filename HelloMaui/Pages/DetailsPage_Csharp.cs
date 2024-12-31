using CommunityToolkit.Maui.Markup;
using HelloMaui.Models;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public class DetailsPage_Csharp : BaseContentPage<DetailsViewModel>
{
    public DetailsPage_Csharp(DetailsViewModel detailsViewModel) : base(detailsViewModel)
    {
        this.Bind(DetailsPage_Csharp.TitleProperty, (DetailsViewModel dvm) => dvm.Title);
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
                    .Bind(Image.SourceProperty, getter: (DetailsViewModel dvm) => dvm.ImageSource),
                new Label()
                    .Font(italic: true, size: 16)
                    .Center()
                    .TextCenter()
                    .Bind(Label.TextProperty, getter: (DetailsViewModel dvm) => dvm.Description),
                new Button()
                    .Text("Back")
                    .Bind(Button.CommandProperty, getter: (DetailsViewModel dvm) => dvm.GoBackCommand)
            }
        }.Padding(12);
    }
}