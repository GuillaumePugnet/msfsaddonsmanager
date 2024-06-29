using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Services;

public class LibraryService : ILibraryService
{
    private readonly IDataService _dataService;

    private readonly ConcurrentDictionary<string, Task> _libraryScanners = [];

    public LibraryService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task ScanLibraryAsync(ObjectId libraryId)
    {
        var library = await _dataService.GetLibraryAsync(libraryId);
        if (library != null)
        {
         
        }
    }
}