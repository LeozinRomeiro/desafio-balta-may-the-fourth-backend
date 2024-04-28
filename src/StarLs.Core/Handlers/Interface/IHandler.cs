namespace StarLs.Core.Handlers.Interface;

public  interface IHandler<TRequest, TResponse>
{
    Task<TResponse> Send(TRequest request, int skip, int take);
    Task<TResponse> Send(TRequest request);
}
