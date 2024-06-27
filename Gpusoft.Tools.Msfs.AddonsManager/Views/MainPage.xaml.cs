using Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Gpusoft.Tools.Msfs.AddonsManager.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
