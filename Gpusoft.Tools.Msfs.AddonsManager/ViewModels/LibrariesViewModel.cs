using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.ViewModels;
using Gpusoft.Tools.Msfs.AddonsManager.Messages;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using Gpusoft.Tools.Msfs.AddonsManager.Services;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

public partial class LibrariesViewModel : 
    ObservableRecipient, 
    INavigationAware
{
    private readonly IDataService _dataService;
    private readonly ILibraryService _libraryService;


    [ObservableProperty]
    private bool _isScanningAllLibraries;

    public ObservableCollection<LibraryViewModel> Libraries { get; private set; } = [];

    public ICommand AddNewLibraryCommand
    {
        get;
    }
    public ICommand ScallAllLibrariesCommand
    {
        get;
    }

    public LibrariesViewModel(IDataService dataService, ILibraryService libraryService)
    {
        _dataService = dataService;
        _libraryService = libraryService;

        AddNewLibraryCommand = new AsyncRelayCommand(async (o) => await OnAddNewLibraryCommandAsync());
        ScallAllLibrariesCommand = new AsyncRelayCommand(async (o) => await OnScanAllLibrariesCommandAsync());       
    }

    public async void OnNavigatedTo(object parameter)
    {
        await LoadLibrariesAsync();
    }

    public void OnNavigatedFrom()
    {

    }

    public async Task OnDeleteLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        await _dataService.DeleteItemAsync<Library>(libraryViewModel.Library);
        await LoadLibrariesAsync();
    }



    public Task OnScanLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        return _libraryService.ScanLibraryAsync(libraryViewModel.Library.LibraryId);
    }

    public Task OnScanAllLibrariesCommandAsync()
    {
        // TODO handle null lib
        var libraryIds = Libraries.Select(l => l.Library.LibraryId).ToList();
        return _libraryService.ScanLibrariesAsync(libraryIds);
    }

    public async Task OnAddNewLibraryCommandAsync()
    {
        var folderPicker = new FolderPicker
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };

        var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(folderPicker, hwnd);

        var folder = await folderPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            var library = new Library() { Path = folder.Path };
            await _dataService.AddItemAsync<Library>(library);
            await LoadLibrariesAsync();            
        }
    }

    private async Task LoadLibrariesAsync()
    {
        Libraries.Clear();
        var libraries = await _dataService.FindItemsAsync<Library>();
        foreach (var l in libraries)
        {
            Libraries.Add(new LibraryViewModel(l, this));
        }
    }
}
