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
    Task AddLibraryAsync(Library library);
    Task DeleteLibraryAsync(Library library);
    Task<Library> GetLibraryAsync(ObjectId libraryId);
    Task<IEnumerable<Library>> GetLibrariesAsync();

    Task AddAddonAsync(Addon addon);
    Task DeleteAddonAsync(Addon addon);


    Task AddCategoryAsync(Category addon);
    Task DeleteCategoryAsync(Category addon);
    Task UpdateCategoryAsync(Category addon);
}
