using PhotoArchiveApp.CoreLib;

var files = FileService.GetFiles(@"D:\123");
var photos = FileService.GetPhotosInfos(files);
FileService.RelocatePhotos(photos, @"D:\Temp");

