using System.Collections.Generic;
using System.Linq;

namespace ClientPlatform
{
    public sealed class TokenizedUrl
    {
        public TokenizedUrl(IEnumerable<string> resourceNames, string fullUrl)
        {
            tokenValueDictionary = resourceNames.ToDictionary(s => s);
            FullUrl = fullUrl;
        }

        private readonly IDictionary<string, string> tokenValueDictionary = new Dictionary<string, string>();

        public IEnumerable<string> ResourceNames => tokenValueDictionary.Keys.ToList();

        public void AddValueToToken(string name, string value)
        {
            tokenValueDictionary[name] = value;
        }

        public string GetResourceValue(string name)
        {
            return tokenValueDictionary[name];
        }

        public string FullUrl { get; }


    }
}