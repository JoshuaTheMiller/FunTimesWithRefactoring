using ServiceStack;
using ServiceStackWebApp.ServiceModel;

namespace ServiceStackWebApp.ServiceInterface
{
    public class MyServices : Service
    {
        public object Get(Hello request)
        {
            if(request.Name == "ThrowNotFound")
            {
                throw new ResourceNotFoundException();
            }

            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}