using Avalonia.Controls;

namespace PhotoArchiveApp.Desktop;

using System.Linq;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CoreLib;

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

    private void ButtonRunRelocate_OnClick(object? sender, RoutedEventArgs e)
    {
        var source = this.InputSource.Text;
        var dist = this.InputDist.Text;
        if (source is null || dist is null)
        {
            return;
        }

        var files = FileService.GetFiles(source);
        var photos = FileService.GetPhotosInfos(files);
        FileService.RelocatePhotos(photos, dist);
    }
}
