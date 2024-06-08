// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using PhotoArchiveApp.CoreLib;

var files = FileService.GetFiles(@"D:\123");
var photos = FileService.GetPhotosInfos(files);
FileService.RelocatePhotos(photos, @"D:\Temp");

