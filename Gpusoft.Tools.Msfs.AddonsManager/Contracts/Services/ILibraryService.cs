﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;

public interface ILibraryService
{
    Task ScanLibraryAsync(ObjectId libraryId);
}
