using System;
using System.Collections.Generic;

namespace DesktopClient.Platform
{
    public interface IAttributeReader
    {
        IEnumerable<T> GetAttribute<T>(Type attributedType);
    }
}
