namespace ClientPlatform
{
    public interface IRouteReader
    {
        ServiceRoute GetRouteFromRequest<T>(T request);
    }
}
