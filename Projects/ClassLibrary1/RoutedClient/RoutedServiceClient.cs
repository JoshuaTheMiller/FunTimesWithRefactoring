using ClientFramework;
using ServiceStack;

namespace ClientPlatform.ServiceStack.RoutedClient
{
    public sealed class RoutedServiceClient : IRoutedServiceClient
    {
        private readonly IServiceClient serviceClient;

        public RoutedServiceClient(IServiceClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request)
        {
            var castedRequest = (IReturn<TResponse>)request;

            var response = this.serviceClient.Send<TResponse>(castedRequest);

            return new RoutedServiceResponse<TResponse>(response, true, string.Empty);
        }
    }
}
