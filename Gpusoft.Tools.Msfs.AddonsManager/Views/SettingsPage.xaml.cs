using Gpusoft.Tools.Msfs.AddonsManager.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Gpusoft.Tools.Msfs.AddonsManager.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }
}
