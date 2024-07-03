using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
using Gpusoft.Tools.Msfs.AddonsManager.Messages;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Services;

public class LibraryService : ILibraryService
{
    private readonly IDataService _dataService;

    private readonly ConcurrentDictionary<ObjectId, (Task Task, CancellationTokenSource CancellationTokenSource)> _tasks = new();

    public LibraryService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public Task ScanLibrariesAsync(IEnumerable<ObjectId> libraryIds)
    {
        return Task.CompletedTask;
    }

    public async Task ScanLibraryAsync(ObjectId libraryId)
    {

        if (false == _tasks.TryGetValue(libraryId, out var scanTask))
        {
            var cancellationTokenSource = new CancellationTokenSource();

            scanTask.CancellationTokenSource = cancellationTokenSource;
            scanTask.Task = Task.Run(async () =>
            {
                try
                {
                    //while (!cancellationTokenSource.Token.IsCancellationRequested)
                    //{
                    // Simulate long-running task
                    await Task.Delay(1000); // e.g., 1 second delay
                                            // }
                    WeakReferenceMessenger.Default.Send<LibraryScanMessage>(new LibraryScanMessage(new LibraryScan(libraryId, LibraryScanState.Completed)));
                }
                catch (OperationCanceledException)
                {
                    // Handle task cancellation
                }
            }, cancellationTokenSource.Token);

            _tasks.TryAdd(libraryId, scanTask);
        }
        else
        {
            switch (scanTask.Task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                case TaskStatus.RanToCompletion:
                    WeakReferenceMessenger.Default.Send<LibraryScanMessage>(new LibraryScanMessage(new LibraryScan(libraryId, LibraryScanState.Completed)));
                    break;

                case TaskStatus.Created:
                case TaskStatus.WaitingForActivation:
                case TaskStatus.WaitingForChildrenToComplete:
                case TaskStatus.WaitingToRun:
                case TaskStatus.Running:
                default:
                    WeakReferenceMessenger.Default.Send<LibraryScanMessage>(new LibraryScanMessage(new LibraryScan(libraryId, LibraryScanState.Scanning)));
                    break;
            }
        }
    }
}