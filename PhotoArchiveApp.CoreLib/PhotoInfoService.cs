// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace PhotoArchiveApp.CoreLib;

using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

public static class PhotoInfoService
{
    public static PhotoInfo GetPhotoInfo(string path)
    {
        var dateTaken = ImageMetadataReader
            .ReadMetadata(path)
            .OfType<ExifSubIfdDirectory>()
            .FirstOrDefault()
            ?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);

        return new PhotoInfo
        {
            FileName = Path.GetFileName(path),
            Path = new Uri(path),
            DateTaken = dateTaken is null
                ? null
                : ConvertToDateTime(dateTaken),
        };
    }

    private static DateTime? ConvertToDateTime(string dateTime)
    {
        var parts = dateTime.Split(' ');
        var date = parts[0].Replace(":", "-");
        var dateStr = $"{date}T{parts[1]}";

        return Convert.ToDateTime(dateStr);
    }
}
