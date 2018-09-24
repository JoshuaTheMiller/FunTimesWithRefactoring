using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Platform
{
    public interface IServiceStackRouteReader
    {
        ServiceStackRoute GetRouteFromRequest<T>(T request);
    }
}
