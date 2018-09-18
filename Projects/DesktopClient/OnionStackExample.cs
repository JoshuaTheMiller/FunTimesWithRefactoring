using ClientFramework;
using ServiceStackCoreApp.ServiceModel;

namespace DesktopClient
{
    public sealed class OnionStackExample : ServiceExample
    {
        private readonly IRoutedServiceClientFactory serviceClientFactory;

        public override string Header { get; } = "Onion Example";

        private int output = 0;

        public OnionStackExample(IRoutedServiceClientFactory serviceClientFactory)
        {
            this.serviceClientFactory = serviceClientFactory;
        }

        protected override void OnExecute(object obj)
        {
            this.output++;
            SearchOutput = output.ToString();

            var request = new Hello
            {
                Name = this.SearchInput
            };

            var serviceClient = serviceClientFactory.Get(this.SourceUrl);

            var response = serviceClient.Send<Hello, HelloResponse>(request);

            if(response.Succeeded)
            {
                SearchOutput = response.Value.Result;
            }
            else
            {
                SearchOutput = "Something went wrong";
            }
        }
    }
}
