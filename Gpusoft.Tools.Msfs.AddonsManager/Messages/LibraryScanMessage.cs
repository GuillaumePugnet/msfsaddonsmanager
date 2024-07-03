using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Gpusoft.Tools.Msfs.AddonsManager.Models;
using Windows.System;

namespace Gpusoft.Tools.Msfs.AddonsManager.Messages;
public class LibraryScanMessage : ValueChangedMessage<LibraryScan>
{
    public LibraryScanMessage(LibraryScan libraryScan) : base(libraryScan)
    {
    }
}
