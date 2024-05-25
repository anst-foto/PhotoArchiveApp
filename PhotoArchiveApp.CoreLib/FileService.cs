namespace PhotoArchiveApp.CoreLib;

public static class FileService
{
    public static IEnumerable<string> GetFiles(string pathDirectory)
    {
        var direcory = new DirectoryInfo(pathDirectory);
        var files = direcory.GetFiles();
        return files.Where(IsPhoto).Select(f => f.FullName);
    }

    public static IEnumerable<PhotoInfo> GetPhotosInfos(IEnumerable<string> files) => files.Select(PhotoInfoService.GetPhotoInfo);

    public static void RelocatePhotos(IEnumerable<PhotoInfo> photos, string newPath)
    {
        foreach (var photo in photos)
        {
            var oldPathFile = photo.Path.AbsolutePath;

            var year = photo.DateTaken.Value.Year;
            if (!Directory.Exists(@$"{newPath}\{year}"))
            {
                Directory.CreateDirectory(@$"{newPath}\{year}");
            }

            var month = $"{photo.DateTaken.Value.Month:00}";
            if (!Directory.Exists(@$"{newPath}\{year}\{year}-{month}"))
            {
                Directory.CreateDirectory(@$"{newPath}\{year}\{year}-{month}");
            }

            var day = $"{photo.DateTaken.Value.Day:00}";
            if (!Directory.Exists(@$"{newPath}\{year}\{year}-{month}\{year}-{month}-{day}"))
            {
                Directory.CreateDirectory(@$"{newPath}\{year}\{year}-{month}\{year}-{month}-{day}");
            }

            var dateTime = photo.DateTaken.Value.ToString("yyyy-MM-dd_HH-mm-ss");
            var ext = Path.GetExtension(oldPathFile);
            var newPathFile = @$"{newPath}\{year}\{year}-{month}\{year}-{month}-{day}\{dateTime}{ext}";

            File.Copy(oldPathFile, newPathFile);
        }
    }

    private static bool IsPhoto(FileInfo file)
    {
        var ext = file.Extension.ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".tiff" or ".tif" or ".webp" or ".heic" or ".heif"
                or ".nef" => true,
            _ => false
        };
    }
}
