using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Models;
public class LibraryScan
{
    public ObjectId LibraryId
    {
        get; set;
    }
    public LibraryScanState State
    {
        get; set;
    }

    public LibraryScan(ObjectId libraryId, LibraryScanState state)
    {
        LibraryId = libraryId;
        State = state;
    }
}

public enum LibraryScanState
{
    None,
    Scanning,
    Completed
}