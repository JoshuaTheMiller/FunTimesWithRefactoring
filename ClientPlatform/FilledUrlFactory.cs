using System.Collections.Generic;

namespace ClientPlatform
{
    public sealed class FilledUrlFactory : IFilledUrlFactory
    {
        private readonly IUrlTokenizer urlTokenizer;
        private readonly IObjectToDictionaryMapper objectToDictionaryMapper;

        public FilledUrlFactory(IUrlTokenizer urlTokenizer, IObjectToDictionaryMapper objectToDictionaryMapper)
        {
            this.urlTokenizer = urlTokenizer;
            this.objectToDictionaryMapper = objectToDictionaryMapper;
        }

        public string GetFilledUrl<T>(T request, string fullUrl, RouteVerb routeVerb)
        {
            IDictionary<string, string> requestPropertiesDictionary = objectToDictionaryMapper.Convert(request);

            var tokenizedUrl = urlTokenizer.Tokenize(fullUrl, requestPropertiesDictionary);

            if (routeVerb == RouteVerb.Get)
            {
                // need the ability to add query string parameters for GET
            }

            var filledUrl = tokenizedUrl.GetFilledUrl();
            return filledUrl;
        }
    }
}
