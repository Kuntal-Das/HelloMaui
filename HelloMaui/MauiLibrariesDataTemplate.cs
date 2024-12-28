using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMaui;

public class MauiLibrariesDataTemplate() : DataTemplate(CreateGridTemplate)
{
    private const int imageRadius = 25;
    private const int imagePadding = 8;

    enum Column
    {
        Icon,
        Text
    }

    enum Row
    {
        Title,
        Description,
        BottomPadding
    }

    static Grid CreateGridTemplate() => new Grid()
    {
        RowSpacing = 12,
        RowDefinitions = Rows.Define(
            (Row.Title, 22),
            (Row.Description, 44),
            (Row.BottomPadding, 8)
        ),

        ColumnDefinitions = Columns.Define(
            (Column.Icon, (imageRadius * 2) + (imagePadding * 2)),
            (Column.Text, Star)
        ),
        Children =
        {
            new Image()
                .Row(Row.Title).RowSpan(All<Row>()).Column(Column.Icon)
                .Center()
                .Aspect(Aspect.AspectFit)
                .Size(imageRadius * 2)
                .Bind(Image.SourceProperty, getter: (LibraryModel m) => m.ImageSource, mode: BindingMode.OneWay),
            new Label()
                {
                    VerticalTextAlignment = TextAlignment.Center
                }
                .Row(Row.Title).Column(Column.Text)
                .Font(size: 18, bold: true)
                .TextColor(Color.FromArgb("#262626"))
                .Bind(Label.TextProperty, getter: (LibraryModel m) => m.Title, mode: BindingMode.OneWay),
            new Label()
                {
                    MaxLines = 2,
                    LineBreakMode = LineBreakMode.WordWrap,
                    VerticalTextAlignment = TextAlignment.Center
                }
                .Row(Row.Description).Column(Column.Text)
                .Font(size: 12)
                .TextColor(Color.FromArgb("#595959"))
                .Bind(Label.TextProperty, getter: (LibraryModel m) => m.Description, mode: BindingMode.OneWay)
        }
    }.Paddings(left: 12, right: 12);
}