//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommunityToolkit.Mvvm.Messaging;
//using Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;
//using Gpusoft.Tools.Msfs.AddonsManager.Messages;
//using Windows.ApplicationModel.Background;
//using Windows.Foundation;
//using Windows.Storage;
//using Windows.Storage.Search;

//namespace Gpusoft.Tools.Msfs.AddonsManager
//{
//    public class LibraryScanner
//    {
//        private readonly IDataService _dataService;
//        private Task _scannerTask;
//        public bool IsScanning
//        {
//            get; private set;
//        }

//        public string Path
//        {
//            get; private set;
//        }

//        public LibraryScanner(IDataService dataService, string path)
//        {
//            _dataService = dataService;
//            Path = path;
//        }

//        public void Start()
//        {
//            IsScanning = true;
//            WeakReferenceMessenger.Default.Send<LibraryScanMessage>(new LibraryMessage(this));

//            _scannerTask = Task.Run(async () =>
//            {
//                var libraryFolder = await StorageFolder.GetFolderFromPathAsync(Path);
//                var addonsQuery = new QueryOptions()
//                {
//                    FolderDepth = FolderDepth.Deep,
//                    IndexerOption = IndexerOption.UseIndexerWhenAvailable
//                };

//                addonsQuery.FileTypeFilter.Add(".rar");
//                addonsQuery.FileTypeFilter.Add(".zip");
//                addonsQuery.FileTypeFilter.Add(".7z");

//                uint addonsIndex = 0;
//                const uint stepSize = 500;
//                var addonsStorageFiles = new List<StorageFile>();
//                var addonsQueryResult = libraryFolder.CreateFileQueryWithOptions(addonsQuery);
//                var addonsFiles = await addonsQueryResult.GetFilesAsync(addonsIndex, stepSize);
//                addonsStorageFiles.AddRange(addonsFiles);

//                while (addonsFiles.Count != 0)
//                {
//                    addonsIndex += stepSize;
//                    addonsFiles = await addonsQueryResult.GetFilesAsync(addonsIndex, stepSize);
//                    addonsStorageFiles.AddRange(addonsFiles);
//                }

//                foreach (var file in addonsStorageFiles)
//                {

//                }

//            }).ContinueWith((previousTask) =>
//            {
//                IsScanning = false;
//                WeakReferenceMessenger.Default.Send<LibraryScanMessage>(new LibraryServiceMessage(this));
//            });
//        }
//    }
//}
