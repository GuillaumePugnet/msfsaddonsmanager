using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Models;

public class Addon
{
    [BsonId]
    public ObjectId AddonId
    {
        get; set;
    }

    public ObjectId LibraryId
    {
        get; set;
    }

    public Addon(ObjectId libraryId)
    {
        LibraryId = libraryId;
    }
}

