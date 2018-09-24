using System;
using ClientFramework;
using DesktopClient.Platform;

namespace ClientPlatform.ServiceStack.RoutedClient
{
    public interface IWebClient : IDisposable
    {
        RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb);
    }
}