using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
        private readonly IUrlTokenizer urlTokenizer;
        private readonly ITokenizedUrlFiller tokenizedUrlFiller;

        public WebClient(HttpClient httpClient, IStringSerializer stringSerializer, IStringDeserializer stringDeserializer, IUrlTokenizer urlTokenizer, ITokenizedUrlFiller tokenizedUrlFiller)
        {
            this.httpClient = httpClient;
            this.stringSerializer = stringSerializer;
            this.stringDeserializer = stringDeserializer;
            this.urlTokenizer = urlTokenizer;
            this.tokenizedUrlFiller = tokenizedUrlFiller;
        } 

        public RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb)
        {
            IDictionary<string, string> requestPropertiesDictionary = GetRequestPropertiesDictionary(request);

            var tokenizedUrl = urlTokenizer.Tokenize(fullUrl, requestPropertiesDictionary);

            var filledUrl = tokenizedUrlFiller.Fill(tokenizedUrl);

            var requestAsString = new StringContent(stringSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var verb = MapVerb(routeVerb);

            var httpMessageRequest = new HttpRequestMessage();
            httpMessageRequest.Method = verb;
            httpMessageRequest.Headers.Add("accept", "application/json");

            if(routeVerb != RouteVerb.Get && routeVerb != RouteVerb.Delete)
            {
                httpMessageRequest.Content = requestAsString;
            }


            if(routeVerb == RouteVerb.Get)
            {
                // need the ability to add query string parameters for GET
            }

            httpMessageRequest.RequestUri = new Uri(filledUrl);

            var httpResponse = Task.Run(async () => await httpClient.SendAsync(httpMessageRequest)).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RoutedServiceResponse<TResponse>(default(TResponse), false, httpResponse.ReasonPhrase);
            }

            var responseAsString = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()).Result;

            return new RoutedServiceResponse<TResponse>(stringDeserializer.Deserialize<TResponse>(responseAsString), true, string.Empty);
        }

        private object FillUrlWithValues(string fullUrl, IDictionary<string, object> propertiesDictionary)
        {
            throw new NotImplementedException();
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

        private IDictionary<string, string> GetRequestPropertiesDictionary<T>(T obj)
        {
            return obj?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
              .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null).ToString());
        }
    }
}
