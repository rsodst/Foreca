namespace Foreca.Shared;

public interface IAutoRegisteredService
{
}

public interface IAutoRegisteredService<TImplementationType> : IAutoRegisteredService
{
}