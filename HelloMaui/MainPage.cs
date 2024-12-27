using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;

namespace HelloMaui;

public class MainPage : BaseContentPage
{
    private const int imageWidthRequest = 500;
    private const int imageHeightRequest = 250;
    private const int labelHeightRequest = 32;
    private const int entryHeightRequest = 48;
    private const int absoluteLayoutChildrenSpacing = 12;

    public MainPage()
    {
        BackgroundColor = Colors.DarkViolet;
        Title = "Hello Maui";
        Content = new AbsoluteLayout()
        {
            // Spacing = 12,
            BackgroundColor = Colors.LightSteelBlue,
            Children =
            {
                new Image()
                    .Size(imageWidthRequest, imageHeightRequest)
                    .Aspect(Aspect.AspectFill)
                    .Source("dotnet_bot")
                    .LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
                    .LayoutBounds(0.5, 0),
                new Label()
                    .Height(labelHeightRequest)
                    .Text("Hello MAUI!")
                    .TextColor(Colors.Black)
                    .Center()
                    .TextCenter()
                    .LayoutFlags(AbsoluteLayoutFlags.XProportional)
                    .LayoutBounds(0.5, imageHeightRequest + absoluteLayoutChildrenSpacing),

                new Entry()
                    .Placeholder("First Entry", Colors.DarkGray)
                    .TextColor(Colors.Black)
                    .LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
                    .LayoutBounds(0, imageHeightRequest + labelHeightRequest + (absoluteLayoutChildrenSpacing * 2), 0.3,
                        entryHeightRequest),

                new Entry()
                    .Placeholder("Second Entry", Colors.DarkGray)
                    .TextColor(Colors.Black)
                    .LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
                    .LayoutBounds(0.5, imageHeightRequest + labelHeightRequest + (absoluteLayoutChildrenSpacing * 2), 0.3,
                        entryHeightRequest),

                new Entry()
                    .Placeholder("Third Entry", Colors.DarkGray)
                    .TextColor(Colors.Black)
                    .LayoutFlags(AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.WidthProportional)
                    .LayoutBounds(1, imageHeightRequest + labelHeightRequest + (absoluteLayoutChildrenSpacing * 2), 0.3,
                        entryHeightRequest),
            }
        };
    }
}