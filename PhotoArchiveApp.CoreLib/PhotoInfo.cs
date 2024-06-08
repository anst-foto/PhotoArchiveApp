// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace PhotoArchiveApp.CoreLib;

public record PhotoInfo
{
    public string FileName { get; init; }
    public Uri Path { get; init; }
    public DateTime? DateTaken { get; init; }
}
