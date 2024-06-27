namespace Gpusoft.Tools.Msfs.AddonsManager.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
