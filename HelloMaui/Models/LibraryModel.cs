using System.Threading.Channels;

namespace HelloMaui.Models;

public class LibraryModel : IEquatable<LibraryModel>
{
    public override string ToString()
    {
        return $"{nameof(Title)}: {Title}, {nameof(Description)}: {Description}, {nameof(ImageSource)}: {ImageSource}";
    }

    public bool Equals(LibraryModel? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Title == other.Title && Description == other.Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LibraryModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Description);
    }

    public static bool operator ==(LibraryModel? left, LibraryModel? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LibraryModel? left, LibraryModel? right)
    {
        return !Equals(left, right);
    }

    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageSource { get; init; } = string.Empty;
}