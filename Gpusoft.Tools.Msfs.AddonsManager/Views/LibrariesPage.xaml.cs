using System.Windows.Input;
using Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Gpusoft.Tools.Msfs.AddonsManager.Views;

public sealed partial class LibrariesPage : Page
{
    public LibrariesViewModel ViewModel
    {
        get;
    }

    public LibrariesPage()
    {
        ViewModel = App.GetService<LibrariesViewModel>();
        InitializeComponent();
    }
}
