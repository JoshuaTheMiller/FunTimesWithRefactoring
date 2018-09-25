using System.Collections.Generic;

namespace ClientPlatform
{
    public interface IObjectToDictionaryMapper
    {
        IDictionary<string, string> Convert<T>(T obj);
    }
}