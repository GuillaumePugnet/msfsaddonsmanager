using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gpusoft.Tools.Msfs.AddonsManager.Messages;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using Newtonsoft.Json;

namespace Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

public partial class LibraryViewModel : ObservableObject, IRecipient<LibraryScanMessage>
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

        WeakReferenceMessenger.Default.Register<LibraryScanMessage>(this);
    }

    private async Task OnScanLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        IsScanning = true;
        await _parentViewModel.OnScanLibraryCommandAsync(libraryViewModel);
    }

    private async Task OnDeleteLibraryCommandAsync(LibraryViewModel? libraryViewModel)
    {
        await _parentViewModel.OnDeleteLibraryCommandAsync(libraryViewModel);
    }

    public void Receive(LibraryScanMessage message)
    {
        if (message.Value.LibraryId == _model.LibraryId)
        {
            switch (message.Value.State)
            {
                case LibraryScanState.Completed:
                case LibraryScanState.None:
                    IsScanning = false;
                    break;

                case LibraryScanState.Scanning:
                    IsScanning = true;
                    break;
            }
        }
    }
}
