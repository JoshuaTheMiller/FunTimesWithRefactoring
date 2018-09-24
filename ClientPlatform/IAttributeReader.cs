using System;
using System.Collections.Generic;

namespace ClientPlatform
{
    public interface IAttributeReader
    {
        IEnumerable<T> GetAttribute<T>(Type attributedType);
    }
}
