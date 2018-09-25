using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClientPlatform
{
    public sealed class ObjectToDictionaryMapper : IObjectToDictionaryMapper
    {
        public IDictionary<string, string> Convert<T>(T obj)
        {
            return obj?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
              .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null).ToString());
        }
    }
}
