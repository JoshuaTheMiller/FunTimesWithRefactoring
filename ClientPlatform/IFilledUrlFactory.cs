namespace ClientPlatform
{
    public interface IFilledUrlFactory
    {
        string GetFilledUrl<T>(T request, string fullUrl, RouteVerb routeVerb);
    }
}
