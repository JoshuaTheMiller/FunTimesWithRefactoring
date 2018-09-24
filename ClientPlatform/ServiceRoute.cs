namespace ClientPlatform
{
    public sealed class ServiceRoute
    {
        public ServiceRoute(string route, RouteVerb verb)
        {
            Route = route;
            Verb = verb;
        }

        public string Route { get; }
        public RouteVerb Verb { get; }
    }
}