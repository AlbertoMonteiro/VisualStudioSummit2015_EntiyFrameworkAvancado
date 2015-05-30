namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public interface ICommandTreeHandler<T>
    {
        T HandleRequest(T command);
        ICommandTreeHandler<T> SetNextHandler(ICommandTreeHandler<T> nextHandler);
    }
}