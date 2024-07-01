using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gpusoft.Tools.Msfs.AddonsManager.Helpers;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Models;

[Collection("libraries")]
public class Library
{
    [BsonId]
    public ObjectId LibraryId
    {
        get; set;
    }

    public string Path
    {
        get; set;
    }

    public DateTime? UpdatedOn
    {
        get; set;
    }
}
