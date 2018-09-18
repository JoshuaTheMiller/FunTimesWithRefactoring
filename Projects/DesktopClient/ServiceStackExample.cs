using ServiceStack;
using ServiceStackCoreApp.ServiceModel;

namespace DesktopClient
{
    public sealed class ServiceStackExample : ServiceExample
    {
        public override string Header { get; } = "Service Stack Way";

        private int output = 0;

        protected override void OnExecute(object obj)
        {
            this.output++;
            SearchOutput = output.ToString();

            var request = new Hello
            {
                Name = this.SearchInput
            };

            var client = new JsonServiceClient(SourceUrl);            

            try
            {
                var response = client.Get(request);
                this.SearchOutput = response.Result;
            }
            catch(WebServiceException ex)
            {
                this.SearchOutput = "Something went wrong";

                // I've seen logging be done here
                // I've also seen a catch all exception block be placed
                // so that all exceptions are swallowed...
            }            
        }
    }
}
