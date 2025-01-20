using System.Threading.Channels;

namespace HelloMaui.Models;

public class LibraryModel
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string ImageSource { get; init; }
}