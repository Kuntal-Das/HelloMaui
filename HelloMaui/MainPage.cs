using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;

namespace HelloMaui;

internal enum Row
{
    Image,
    Label,
    Entry,
    LargeTextLabel
}

internal enum Column
{
    Entry1,
    Entry2,
    Entry3
}

public class MainPage : BaseContentPage
{
    private static readonly string longText =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu lectus semper, porta ligula ac, semper nibh. Aliquam congue a nisi a aliquet. Praesent cursus arcu ac tincidunt sodales. Vivamus sagittis, lacus nec imperdiet interdum, libero odio imperdiet ante, non finibus quam tortor id nibh. Sed pulvinar suscipit est, eget aliquam urna. Morbi a molestie nisl. Vestibulum eu vulputate enim. In at tempor turpis. Donec facilisis finibus nulla, sed rutrum erat tempor ac. Aliquam erat volutpat. Mauris ut nibh a nisi varius auctor at et purus. Pellentesque dui sem, rhoncus in nisi nec, posuere gravida erat.\n\nCurabitur nec ligula et massa accumsan malesuada. Curabitur eu rutrum justo. Integer nec leo nisi. Integer aliquet mollis bibendum. Curabitur vitae molestie turpis. Donec imperdiet faucibus viverra. Duis semper ante urna, at tempor neque eleifend molestie. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec tincidunt varius tortor, eu dictum purus sollicitudin quis. Vestibulum eget augue et est fermentum mattis. Suspendisse tincidunt magna diam, at consectetur urna consequat sed.\n\nSed semper sapien eget tortor rutrum, quis ultrices lacus porttitor. Quisque pellentesque vitae lacus sed eleifend. Ut a quam vitae lorem posuere rhoncus a at massa. Morbi eget luctus arcu. Quisque et magna cursus, vulputate tortor in, feugiat neque. Aenean ullamcorper, nulla sit amet aliquam pretium, neque ligula accumsan augue, ac mattis lacus augue fermentum metus. Donec condimentum varius elit, vitae scelerisque mauris tempus at. Nunc facilisis imperdiet nunc, eget euismod quam pharetra eu. Proin vestibulum pharetra turpis eu maximus. Vestibulum a consectetur nunc. Nulla quis diam lobortis, semper dolor in, posuere eros. Phasellus blandit ipsum a velit porta ultrices. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.\n\nNullam maximus at eros non dignissim. Mauris felis sem, tincidunt vel leo eget, ullamcorper placerat mi. Cras condimentum, turpis quis placerat finibus, leo nisi pulvinar felis, vitae malesuada nisi mi suscipit magna. Nulla sit amet ex libero. Morbi quis molestie justo, quis ultricies risus. In ornare tincidunt ligula eu sagittis. Aliquam erat volutpat. Morbi lacinia luctus purus quis sollicitudin. Donec sodales hendrerit nisi, laoreet pretium quam fermentum nec. In porta vehicula viverra. Morbi sagittis lorem et elementum tempus. In at risus erat. Suspendisse fringilla aliquet quam a mattis. Integer quis lacus nec nisi mattis fermentum nec non enim.\n\nUt vitae bibendum lacus, id molestie odio. Interdum et malesuada fames ac ante ipsum primis in faucibus. Maecenas id arcu gravida nibh mattis volutpat at vel arcu. Duis blandit nibh lacinia odio convallis, in scelerisque tellus pretium. Phasellus vel risus ac sapien accumsan volutpat sit amet id dolor. Morbi tempor et urna ac lobortis. Proin congue tellus vitae sollicitudin ornare. Phasellus pellentesque mattis sem nec fringilla. Vivamus tincidunt lacus nibh, ac consectetur nunc fermentum quis. Nullam pharetra lacus quam, id egestas ex convallis eu. Maecenas vitae lacus et ante convallis tempor sed sodales arcu. Vestibulum convallis interdum dui, sed fermentum neque vehicula sed. Quisque porta nisi nulla, et semper augue dictum vitae. Sed suscipit augue vel metus faucibus, ac volutpat est sollicitudin. Aliquam tempus sem in magna ornare lacinia.";
    // private const int imageWidthRequest = 500;
    // private const int imageHeightRequest = 250;
    // private const int labelHeightRequest = 32;
    // private const int entryHeightRequest = 48;
    // private const int absoluteLayoutChildrenSpacing = 12;

    public MainPage()
    {
        BackgroundColor = Colors.DarkViolet;
        Title = "Hello Maui";
        Content = new ScrollView()
        {
            Content = new Grid()
                {
                    RowSpacing = 12,
                    ColumnSpacing = 12,
                    BackgroundColor = Colors.LightSteelBlue,
                    RowDefinitions = GridRowsColumns.Rows.Define(
                        (Row.Image, GridLength.Star),
                        (Row.Label, GridLength.Auto),
                        (Row.Entry, 40),
                        (Row.LargeTextLabel, GridLength.Star)
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
                            .BackgroundColor(Colors.Green)
                            .Paddings(5, 10, 15, 30)
                            .Margins(30, 15, 10, 5)
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
                            .Column(Column.Entry3),

                        new Label()
                            {
                                LineBreakMode = LineBreakMode.WordWrap,
                            }
                            .Text(longText)
                            .TextCenter()
                            .Row(Row.LargeTextLabel)
                            .ColumnSpan(All<Column>()),
                    }
                }.Top().CenterHorizontal()
                .Padding(12)
                .Margins(0, 6, 0, 0)
        };
    }
}