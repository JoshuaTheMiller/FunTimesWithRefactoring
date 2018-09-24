using System.Collections.Generic;

namespace ClientPlatform
{
    public interface IUrlTokenizer
    {
        TokenizedUrl Tokenize(string fullUrl, IDictionary<string, string> requestPropertiesDictionary);
    }
}