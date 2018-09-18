using System;
using ServiceStack;
using ServiceStackCoreApp.ServiceModel;

namespace ServiceStackCoreApp.ServiceInterface
{
    public class MyServices : Service
    {
        public object Get(Hello request)
        {
            if (request.Name == "ThrowNotFound")
            {
                throw new ResourceNotFoundException();
            }

            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
