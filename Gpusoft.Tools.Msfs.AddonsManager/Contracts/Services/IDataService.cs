using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;

public interface IDataService
{
    Task AddItemAsync<T>(T item);
    Task<T> FindItemAsync<T>(ObjectId objectId);
    Task DeleteItemAsync<T>(T item);
    Task DeleteItemAsync<T>(ObjectId objectId);
    Task<bool> UpdateItemAsync<T>(T item);
    Task<IEnumerable<T>> FindItemsAsync<T>();
}
