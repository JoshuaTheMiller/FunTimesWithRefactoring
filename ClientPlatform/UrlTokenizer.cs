using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace ClientPlatform
{
    public sealed class UrlTokenizer : IUrlTokenizer
    {
        private readonly ITokenizedUrlFulfiller tokenizedUrlFulfiller;

        public UrlTokenizer(ITokenizedUrlFulfiller tokenizedUrlFulfiller)
        {
            this.tokenizedUrlFulfiller = tokenizedUrlFulfiller;
        }

        public TokenizedUrl Tokenize(string fullUrl, IDictionary<string, string> requestPropertiesDictionary)
        {
            var propertiesToFill = FindPropertiesToFill(fullUrl);

            var tokenizedUrl = new TokenizedUrl(propertiesToFill, fullUrl, tokenizedUrlFulfiller);

            foreach (var property in propertiesToFill)
            {
                tokenizedUrl.AddValueToToken(property, requestPropertiesDictionary[property]);
            }

            return tokenizedUrl;
        }

        private IEnumerable<string> FindPropertiesToFill(string fullUrl)
        {
            var matches = Regex.Matches(fullUrl, @"(?<=\{).+?(?=\})", RegexOptions.IgnoreCase);

            var tokenList = new List<string>();

            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    tokenList.Add(capture.Value);
                }
            }

            return tokenList.Distinct().ToList();
        }
    }
}
