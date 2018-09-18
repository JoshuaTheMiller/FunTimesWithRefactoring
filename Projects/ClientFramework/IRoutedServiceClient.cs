namespace ClientFramework
{
    public interface IRoutedServiceClient
    {
        RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request);
    }
}
