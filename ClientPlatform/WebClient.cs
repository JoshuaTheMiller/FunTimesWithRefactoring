using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientFramework;

namespace ClientPlatform
{
    public sealed class WebClient : IWebClient
    {
        private readonly HttpClient httpClient;
        private readonly IStringSerializer stringSerializer;
        private readonly IStringDeserializer stringDeserializer;
        private readonly IFilledUrlFactory filledUrlRetriever;

        public WebClient(HttpClient httpClient, IStringSerializer stringSerializer, IStringDeserializer stringDeserializer, IFilledUrlFactory filledUrlRetriever)
        {
            this.httpClient = httpClient;
            this.stringSerializer = stringSerializer;
            this.stringDeserializer = stringDeserializer;
            this.filledUrlRetriever = filledUrlRetriever;
        }

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb)
        {
            string filledUrl = filledUrlRetriever.GetFilledUrl(request, fullUrl, routeVerb);

            var webRequest = WebRequest.Create(filledUrl);

            webRequest.Method = routeVerb.ToString();
            webRequest.Headers.Add("accept", "application/json");

            if (routeVerb != RouteVerb.Get && routeVerb != RouteVerb.Delete)
            {
                var content = stringSerializer.Serialize(request);
                var contentAsByteArray = Encoding.UTF8.GetBytes(content);

                webRequest.ContentType = "application/json";
                webRequest.ContentLength = contentAsByteArray.Length;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(contentAsByteArray, 0, contentAsByteArray.Length);
                }
            }

            HttpWebResponse webResponse = null;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException exception)
            {                               
                if(exception.Status != WebExceptionStatus.ProtocolError)
                {
                    throw exception;
                }

                webResponse = (HttpWebResponse)exception.Response;
            }

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                return new RoutedServiceResponse<TResponse>(default(TResponse), false, webResponse.StatusDescription);
            }

            string responseAsString = string.Empty;

            using (var responseData = webResponse.GetResponseStream())
            using (var streamReader = new StreamReader(responseData))
            {
                responseAsString = streamReader.ReadToEnd();
            }

            return CreateSuccessResponse<TResponse>(responseAsString);
        }

        private async Task<RoutedServiceResponse<TResponse>> SendAsync<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb)
        {
            string filledUrl = filledUrlRetriever.GetFilledUrl(request, fullUrl, routeVerb);

            var verb = MapVerb(routeVerb);

            var httpMessageRequest = new HttpRequestMessage(verb, filledUrl);
            httpMessageRequest.Headers.Add("accept", "application/json");

            if (routeVerb != RouteVerb.Get && routeVerb != RouteVerb.Delete)
            {
                var requestAsString = new StringContent(stringSerializer.Serialize(request), Encoding.UTF8, "application/json");
                httpMessageRequest.Content = requestAsString;
            }

            var httpResponse = await httpClient.SendAsync(httpMessageRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RoutedServiceResponse<TResponse>(default(TResponse), false, httpResponse.ReasonPhrase);
            }

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();

            return CreateSuccessResponse<TResponse>(responseAsString);
        }

        private RoutedServiceResponse<TResponse> CreateSuccessResponse<TResponse>(string responseAsString)
        {
            return new RoutedServiceResponse<TResponse>(stringDeserializer.Deserialize<TResponse>(responseAsString), true, string.Empty);
        }

        private HttpMethod MapVerb(RouteVerb routeVerb)
        {
            switch (routeVerb)
            {
                case RouteVerb.Post:
                    return HttpMethod.Post;
                case RouteVerb.Get:
                    return HttpMethod.Get;
                case RouteVerb.Put:
                    return HttpMethod.Put;
                case RouteVerb.Delete:
                    return HttpMethod.Delete;
                case RouteVerb.Patch:
                    return new HttpMethod("PATCH");
                default:
                    throw new ArgumentOutOfRangeException(nameof(routeVerb), routeVerb, null);
            }
        }
    }
}
