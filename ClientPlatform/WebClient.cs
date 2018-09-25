using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

            if (routeVerb == RouteVerb.Get)
            {
                // need the ability to add query string parameters for GET
            }

            var webResponse = (HttpWebResponse)webRequest.GetResponse();

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

            return new RoutedServiceResponse<TResponse>(stringDeserializer.Deserialize<TResponse>(responseAsString), true, string.Empty);
        }

        private async Task<RoutedServiceResponse<TResponse>> SendAsync<TRequest, TResponse>(TRequest request, string fullUrl, RouteVerb routeVerb)
        {
            IDictionary<string, string> requestPropertiesDictionary = GetRequestPropertiesDictionary(request);

            var tokenizedUrl = urlTokenizer.Tokenize(fullUrl, requestPropertiesDictionary);

            var filledUrl = tokenizedUrlFiller.Fill(tokenizedUrl);

            var verb = MapVerb(routeVerb);

            var httpMessageRequest = new HttpRequestMessage();
            httpMessageRequest.Method = verb;
            httpMessageRequest.Headers.Add("accept", "application/json");

            if (routeVerb != RouteVerb.Get && routeVerb != RouteVerb.Delete)
            {
                var requestAsString = new StringContent(stringSerializer.Serialize(request), Encoding.UTF8, "application/json");
                httpMessageRequest.Content = requestAsString;
            }

            if (routeVerb == RouteVerb.Get)
            {
                // need the ability to add query string parameters for GET
            }

            httpMessageRequest.RequestUri = new Uri(filledUrl);

            var httpResponse = await httpClient.SendAsync(httpMessageRequest);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RoutedServiceResponse<TResponse>(default(TResponse), false, httpResponse.ReasonPhrase);
            }

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();

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
