using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using Newtonsoft.Json;

namespace Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

public partial class LibraryViewModel : ObservableObject
{

    private readonly LibrariesViewModel _parentViewModel;
    private readonly Library _model;

    [ObservableProperty]
    private bool _isScanning;

    public string Path
    {
        get => _model.Path;
        set
        {
            _model.Path = value;
            OnPropertyChanged();
        }
    }

    public DateTime? UpdatedOn
    {
        get => _model.UpdatedOn;
        set
        {
            _model.UpdatedOn = value;
            OnPropertyChanged();
        }
    }

    public Library Library => _model;

    public DateTime CreatedOn => _model.LibraryId.CreationTime;

    public ICommand ScanLibraryCommand
    {
        get;
    }

    public ICommand DeleteLibraryCommand
    {
        get;
    }

    public LibraryViewModel(Library model, LibrariesViewModel parentViewModel)
    {
        _parentViewModel = parentViewModel;
        _model = model;

        ScanLibraryCommand = new AsyncRelayCommand<LibraryViewModel>(async (lvm) =>
        {
            await OnScanLibraryCommandAsync(lvm);
        });

        DeleteLibraryCommand = new AsyncRelayCommand<LibraryViewModel>(async (lvm) =>
        {
            await OnDeleteLibraryCommandAsync(lvm);
        });
    }

    private async Task OnScanLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        IsScanning = true;

        await _parentViewModel.OnScanLibraryCommandAsync(libraryViewModel);

        IsScanning = false;
    }

    private async Task OnDeleteLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        await _parentViewModel.OnDeleteLibraryCommandAsync(libraryViewModel);
    }
}
