namespace ClientPlatform
{
    public sealed class StringToVerbMapper : IStringToVerbMapper
    {
        public RouteVerb MapToVerb(string verb)
        {
            switch (verb.ToUpperInvariant())
            {
                case "GET":
                    return RouteVerb.Get;
                case "POST":
                    return RouteVerb.Post;
                case "PATCH":
                    return RouteVerb.Patch;
                case "DELETE":
                    return RouteVerb.Delete;
                case "PUT":
                    return RouteVerb.Put;
                default:
                    throw new VerbNotRecognizedException(verb);
            }
        }
    }
}
