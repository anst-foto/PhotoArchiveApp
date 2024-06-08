// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace PhotoArchiveApp.Desktop;

using System;
using System.Linq;
using System.Threading.Tasks;

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

using CoreLib;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task<string?> OpenFolderDialogShow()
    {
        var dialogWindow = this.StorageProvider;
        var folders = await dialogWindow.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Выберите папку",
            AllowMultiple = false
        });
        return folders.FirstOrDefault()?.Path.AbsolutePath;
    }

    private async void ButtonSource_OnClick(object? sender, RoutedEventArgs e) => this.InputSource.Text = await this.OpenFolderDialogShow();
    private async void ButtonDist_OnClick(object? sender, RoutedEventArgs e) => this.InputDist.Text = await this.OpenFolderDialogShow();

    private void ButtonTest_OnClick(object? sender, RoutedEventArgs e) => throw new ArgumentNullException(nameof(sender));

    private async void ButtonRunRelocate_OnClick(object? sender, RoutedEventArgs e)
    {
        var source = this.InputSource.Text;
        var dist = this.InputDist.Text;
        if (source is null || dist is null)
        {
            return;
        }

        var progress = new Progress<int>();
        progress.ProgressChanged += (s, i) =>
        {
            this.ProgressRelocating.Value = i;
        };

        var files = FileService.GetFiles(source);
        var photos = FileService.GetPhotosInfos(files);
        this.ProgressRelocating.Minimum = 0;
        this.ProgressRelocating.Maximum = photos.Count();
        try
        {
            var result = FileService.RelocatePhotos(photos, dist, progress);
            if (result)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("", "Успешно перемещено")
                    .ShowAsync();
            }
        }
        catch (Exception exception)
        {
            await MessageBoxManager
                .GetMessageBoxStandard("", $"Ошибка перемещения. {exception.Message}")
                .ShowAsync();
        }
    }
}
