using ClientFramework;

namespace ClientPlatform
{
    public interface IWebClient
    {
        RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb);
    }
}