using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientPlatform
{
    public sealed class AttributeReader : IAttributeReader
    {
        public IEnumerable<T> GetAttribute<T>(Type attributedType)
        {
            return attributedType.GetCustomAttributes(typeof(T), true).Cast<T>();
        }
    }
}
