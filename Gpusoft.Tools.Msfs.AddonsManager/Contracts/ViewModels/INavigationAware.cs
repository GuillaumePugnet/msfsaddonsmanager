namespace Gpusoft.Tools.Msfs.AddonsManager.Contracts.ViewModels;

public interface INavigationAware
{
    void OnNavigatedTo(object parameter);

    void OnNavigatedFrom();
}
