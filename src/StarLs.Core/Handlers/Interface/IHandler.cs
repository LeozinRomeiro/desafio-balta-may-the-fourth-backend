namespace StarLs.Core.Handlers.Interface;

public  interface IHandler<TRequest, TResponse>
{
    Task<TResponse> Send(TRequest request);
}
