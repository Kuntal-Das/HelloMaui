using CommunityToolkit.Maui.Markup;

namespace HelloMaui;

public class MainPage : BaseContentPage
{
    public MainPage()
    {
        Title = "Hello Maui";
        Content = new VerticalStackLayout()
        {
            Children =
            {
                new Image()
                    .Size(500, 250)
                    .Aspect(Aspect.AspectFill)
                    .Source("dotnet_bot"),
                new Label()
                    .Text("Hello MAUI!")
                    .TextColor(Colors.Black)
                    .Center()
                    .TextCenter(),
                new Entry()
                    .Placeholder("Notes", Colors.DarkGray)
                    .TextColor(Colors.Black)
            }
        };
    }
}