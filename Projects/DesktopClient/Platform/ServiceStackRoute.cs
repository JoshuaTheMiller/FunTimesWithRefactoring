namespace DesktopClient.Platform
{
    public sealed class ServiceStackRoute
    {
        public ServiceStackRoute(string route, RouteVerb verb)
        {
            Route = route;
            Verb = verb;
        }

        public string Route { get; }
        public RouteVerb Verb { get; }
    }
}