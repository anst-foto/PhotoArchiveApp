namespace PhotoArchiveApp.CoreLib;

public record PhotoInfo
{
    public string FileName { get; init; }
    public Uri Path { get; init; }
    public DateTime? DateTaken { get; init; }
}
