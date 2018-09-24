using System.Linq;
using ClientPlatform;
using ServiceStack;

namespace DesktopClient.ClientPlatform
{
    public sealed class ServiceStackRouteReader : IRouteReader
    {
        private readonly IAttributeReader attributeReader;
        private readonly IStringToVerbMapper stringToVerbMapper;

        public ServiceStackRouteReader(IAttributeReader attributeReader, IStringToVerbMapper stringToVerbMapper)
        {
            this.attributeReader = attributeReader;
            this.stringToVerbMapper = stringToVerbMapper;
        }

        public ServiceRoute GetRouteFromRequest<T>(T request)
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

            return new ServiceRoute(attribute.Path, stringToVerbMapper.MapToVerb(verbs[0]));
        }
    }
}
