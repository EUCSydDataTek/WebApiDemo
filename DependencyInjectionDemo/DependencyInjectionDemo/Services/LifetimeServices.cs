namespace DependencyInjectionDemo.Services;

public interface IService
{
    string Name { get; }
    string SayHello();
}

#region TransientService
public interface ITransientService : IService
{
}
public class TransientService : ITransientService
{
    private readonly Guid _serviceId;
    private readonly DateTime _createdAt;
    public TransientService()
    {
        _serviceId = Guid.NewGuid();
        _createdAt = DateTime.Now;
    }
    public string Name => nameof(TransientService);
    public string SayHello()
    {
        return $"Hello! I am {Name}. My Id is {_serviceId}. I was created at {_createdAt: yyyy - MM - dd HH: mm: ss}.";
    }
}
#endregion

#region SingletonService
public interface ISingletonService : IService
{
}
public class SingletonService : ISingletonService
{
    private readonly Guid _serviceId;
    private readonly DateTime _createdAt;
    public SingletonService()
    {
        _serviceId = Guid.NewGuid();
        _createdAt = DateTime.Now;
    }
    public string Name => nameof(SingletonService);
    public string SayHello()
    {
        return $"Hello! I am {Name}. My Id is {_serviceId}. I was created at {_createdAt: yyyy - MM - dd HH: mm: ss}.";
    }
}
#endregion

#region ScopedService
public interface IScopedService : IService
{
}
public class ScopedService : IScopedService
{
    private readonly Guid _serviceId;
    private readonly DateTime _createdAt;
    private readonly ITransientService _transientService;
    private readonly ISingletonService _singletonService;
    public ScopedService(ITransientService transientService,
    ISingletonService singletonService)
    {
        _transientService = transientService;
        _singletonService = singletonService;
        _serviceId = Guid.NewGuid();
        _createdAt = DateTime.Now;
    }
    public string Name => nameof(ScopedService);
    public string SayHello()
    {
        var scopedServiceMessage = $"Hello! I am {Name}.    My Id is {_serviceId}. I was created at {_createdAt: yyyy - MM - dd HH: mm: ss}.";
        var transientServiceMessage = $"{_transientService.
        SayHello()} I am from {Name}.";
        var singletonServiceMessage = $"{_singletonService.
        SayHello()} I am from {Name}.";
        return $"{scopedServiceMessage}{Environment.NewLine}{transientServiceMessage}{Environment.NewLine}{singletonServiceMessage} ";
    }
}
#endregion