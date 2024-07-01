using System.Collections.Concurrent;
using System.Reflection;
using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
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
    private readonly LiteDatabaseAsync _db;

    private readonly string _applicationDataFolder;
    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _databasePath;
    private readonly ConcurrentDictionary<Type, string> _CollectionNames = new();
    private readonly ConcurrentDictionary<Type, PropertyInfo> _bsonIdProperties = new();

    public DataService(IOptions<LocalSettingsOptions> options)
    {
        _options = options.Value;
        _applicationDataFolder = Path.Combine(_localApplicationData, _options.ApplicationDataFolder ?? Constants.DefaultApplicationDataFolder);

        if (RuntimeHelper.IsMSIX)
        {
            _databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.DatabaseFilename);
        }
        else
        {
            Directory.CreateDirectory(_applicationDataFolder);
            _databasePath = Path.Combine(_applicationDataFolder, Constants.DatabaseFilename);
        }
        _db = new LiteDatabaseAsync($"Filename={_databasePath};Connection=shared;");
    }

    public Task AddItemAsync<T>(T item)
    {
        var collectionName = GetCollectionName<T>();

        return _db.GetCollection<T>(collectionName).InsertAsync(item);
    }

    public Task<T> FindItemAsync<T>(ObjectId objectId)
    {
        var collectionName = GetCollectionName<T>();

        return _db.GetCollection<T>(collectionName).FindByIdAsync(objectId);
    }

    public Task DeleteItemAsync<T>(T item)
    {
        var collectionName = GetCollectionName<T>();
        var objectId = GetObjectId<T>(item);

        return _db.GetCollection<T>(collectionName).DeleteAsync(objectId);
    }

    public Task DeleteItemAsync<T>(ObjectId objectId)
    {
        var collectionName = GetCollectionName<T>();

        return _db.GetCollection<T>(collectionName).DeleteAsync(objectId);
    }

    public Task<bool> UpdateItemAsync<T>(T item)
    {
        var collectionName = GetCollectionName<T>();

        return _db.GetCollection<T>(collectionName).UpdateAsync(item);
    }

    public Task<IEnumerable<T>> FindItemsAsync<T>()
    {
        var collectionName = GetCollectionName<T>();

        return _db.GetCollection<T>(collectionName).FindAllAsync();
    }

    private string GetCollectionName<T>()
    {
        var type = typeof(T);
        if (false == _CollectionNames.TryGetValue(type, out var collectionName))
        {
            var attributes = Attribute.GetCustomAttributes(typeof(T));
            if (attributes
                .Where(a => a is CollectionAttribute)
                .SingleOrDefault() is CollectionAttribute collectionAttribute)
            {
                collectionName = _CollectionNames.AddOrUpdate(type, collectionAttribute.CollectionName, (k, v) => collectionAttribute.CollectionName);
            }
            else
            {
                throw new MissingAttributeException(typeof(CollectionAttribute));
            }
        }

        return collectionName;
    }

    private ObjectId GetObjectId<T>(T instance)
    {
        var type = typeof(T);
        if (false == _bsonIdProperties.TryGetValue(type, out var property))
        {
            property = type.GetProperties()
            .FirstOrDefault(prop => prop.GetCustomAttributes(typeof(BsonIdAttribute), false).Length > 0);
            if (property != null)
            {
                _bsonIdProperties.TryAdd(type, property);
            }
        }

        return (ObjectId)property?.GetValue(instance);
    }
}
