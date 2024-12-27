using CommunityToolkit.Maui.Markup;

namespace HelloMaui;

public class MainPage : BaseContentPage
{
    public MainPage()
    {
        BackgroundColor = Colors.DarkViolet;
        Title = "Hello Maui";
        Content = new VerticalStackLayout()
        {
            BackgroundColor = Colors.LightSteelBlue,
            Spacing = 12,
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
                new HorizontalStackLayout()
                {
                    BackgroundColor = Colors.ForestGreen,
                    Spacing = 12,
                    Children =
                    {
                        new Entry()
                            .Placeholder("First Entry", Colors.DarkGray)
                            .TextColor(Colors.Black),
                        
                        new Entry()
                            .Placeholder("Second Entry", Colors.DarkGray)
                            .TextColor(Colors.Black),
                        
                        new Entry()
                            .Placeholder("Third Entry", Colors.DarkGray)
                            .TextColor(Colors.Black),
                    },
                }.Center(),
            }
        };
    }
}