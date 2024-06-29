using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
using Gpusoft.Tools.Msfs.AddonsManager.Helpers;
using Gpusoft.Tools.Msfs.AddonsManager.Helpers;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using LiteDB;
using LiteDB.Async;
using Microsoft.Extensions.Options;
using Windows.Storage;

namespace Gpusoft.Tools.Msfs.AddonsManager.Services;

public class DataService : IDataService
{
    private readonly LocalSettingsOptions _options;
    private readonly ILiteDatabaseAsync _db;

    private readonly string _applicationDataFolder;
    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _databasePath;

    public DataService(IOptions<LocalSettingsOptions> options)
    {
        _options = options.Value;

        if (RuntimeHelper.IsMSIX)
        {
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.DatabaseFilename);
        }
        else
        {
            _applicationDataFolder = Path.Combine(_localApplicationData, _options.ApplicationDataFolder ?? Constants.DefaultApplicationDataFolder);
            Directory.CreateDirectory(_applicationDataFolder);
            _databasePath = Path.Combine(_applicationDataFolder, Constants.DatabaseFilename);
        }
        _db = new LiteDatabaseAsync($"Filename={_databasePath};Connection=shared;");
    }

    public Task<IEnumerable<Library>> GetLibrariesAsync()
    {
        return _db.GetCollection<Library>("libraries").FindAllAsync();
    }

    public Task AddLibraryAsync(Library library)
    {
        return _db.GetCollection<Library>("libraries").InsertAsync(library);
    }

    public async Task DeleteLibraryAsync(Library library)
    {
        var localDb = await _db.BeginTransactionAsync();
        await localDb.GetCollection<Library>("libraries").DeleteAsync(library.LibraryId);
        await localDb.GetCollection<Addon>("addons").DeleteManyAsync(a => a.LibraryId == library.LibraryId);
        await localDb.CommitAsync();
    }

    public Task<Library> GetLibraryAsync(ObjectId libraryId)
    {
        return _db.GetCollection<Library>("libraries").FindByIdAsync(libraryId);
    }
    public Task AddAddonAsync(Addon addon)
    {
        return _db.GetCollection<Addon>("addons").InsertAsync(addon);
    }

    public Task DeleteAddonAsync(Addon addon)
    {
        return _db.GetCollection<Addon>("addons").DeleteAsync(addon.AddonId);
    }
}
