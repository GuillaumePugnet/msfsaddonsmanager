using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gpusoft.Tools.Msfs.AddonsManager.Helpers;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Models;
[Collection("categories")]
public class Category
{
    [BsonId]
    public ObjectId CategoriesId
    {
        get; set;
    }

    public string Value
    {
        get; set;
    }

    public string DisplayName
    {
        get; set;
    }
}
