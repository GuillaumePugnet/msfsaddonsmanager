using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;

namespace Gpusoft.Tools.Msfs.AddonsManager
{
    class LibraryScanner
    {
    }

    public sealed class LibraryScannerBackgroundTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral; // Note: defined at class scope so that we can mark it complete
                                          // inside the OnCancel() callback if we choose to support
                                          // cancellation

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //If you run any asynchronous code in your background task, then your background task needs to use a deferral.
            //If you don't use a deferral, then the background task process can terminate unexpectedly if the Run method
            //returns before any asynchronous work has run to completion.
            _deferral = taskInstance.GetDeferral();
            //
            // TODO: Insert code to start one or more asynchronous methods using the
            //       await keyword, for example:
            //
            // await ExampleMethodAsync();
            //

            _deferral.Complete();
        }
    }
}
