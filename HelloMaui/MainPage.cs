using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;

namespace HelloMaui;

internal enum Row
{
    Image,
    Label,
    Entry,
    Empty
}

internal enum Column
{
    Entry1,
    Entry2,
    Entry3
}

public class MainPage : BaseContentPage
{
    // private const int imageWidthRequest = 500;
    // private const int imageHeightRequest = 250;
    // private const int labelHeightRequest = 32;
    // private const int entryHeightRequest = 48;
    // private const int absoluteLayoutChildrenSpacing = 12;

    public MainPage()
    {
        BackgroundColor = Colors.DarkViolet;
        Title = "Hello Maui";
        Content = new Grid()
        {
            RowSpacing = 12,
            ColumnSpacing = 12,
            BackgroundColor = Colors.LightSteelBlue,
            RowDefinitions = GridRowsColumns.Rows.Define(
                (Row.Image, GridLength.Star),
                (Row.Label, 32),
                (Row.Entry, 40)
                // (Row.Empty, GridLength.Star)
            ),
            ColumnDefinitions = GridRowsColumns.Columns.Define(
                (Column.Entry1, GridLength.Star),
                (Column.Entry2, GridLength.Star),
                (Column.Entry3, GridLength.Star)
            ),

            Children =
            {
                new Image()
                    // .Size(500, 250)
                    // .Aspect(Aspect.AspectFill)
                    .Source("dotnet_bot")
                    .Row(Row.Image)
                    .ColumnSpan(3),

                new Label()
                    .Text("Hello MAUI!")
                    .TextColor(Colors.Black)
                    .Center()
                    .TextCenter()
                    .Row(Row.Label)
                    .ColumnSpan(3),

                new Entry()
                    .Placeholder("First Entry", Colors.DarkOliveGreen)
                    .TextColor(Colors.Black)
                    .Row(Row.Entry)
                    .Column(Column.Entry1),

                new Entry()
                    .Placeholder("Second Entry", Colors.DarkOliveGreen)
                    .TextColor(Colors.Black)
                    .Row(Row.Entry)
                    .Column(Column.Entry2),

                new Entry()
                    .Placeholder("Third Entry", Colors.DarkOliveGreen)
                    .TextColor(Colors.Black)
                    .Row(Row.Entry)
                    .Column(Column.Entry3)
            }
        }.Top();
    }
}