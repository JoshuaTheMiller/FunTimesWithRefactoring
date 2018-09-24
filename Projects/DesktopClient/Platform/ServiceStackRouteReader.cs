using System.Linq;
using ServiceStack;

namespace DesktopClient.Platform
{
    public sealed class ServiceStackRouteReader : IServiceStackRouteReader
    {
        private readonly IAttributeReader attributeReader;
        private readonly IStringToVerbMapper stringToVerbMapper;

        public ServiceStackRouteReader(IAttributeReader attributeReader, IStringToVerbMapper stringToVerbMapper)
        {
            this.attributeReader = attributeReader;
            this.stringToVerbMapper = stringToVerbMapper;
        }

        public ServiceStackRoute GetRouteFromRequest<T>(T request)
        {
            var attributes = attributeReader.GetAttribute<RouteAttribute>(typeof(T)).ToList();

            if (attributes.Count != 1)
            {
                // throw specific exception
            }

            var attribute = attributes.First();

            var verbs = attribute.Verbs.Split(',');

            if (verbs.Length != 1)
            {
                // throw specific exception
            }

            return new ServiceStackRoute(attribute.Path, stringToVerbMapper.MapToVerb(verbs[0]));
        }
    }
}
