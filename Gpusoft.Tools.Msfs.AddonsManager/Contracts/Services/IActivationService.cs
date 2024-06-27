namespace Gpusoft.Tools.Msfs.AddonsManager.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
