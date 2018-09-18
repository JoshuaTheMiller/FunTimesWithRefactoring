using ServiceStack;

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
            
            var client = new JsonServiceClient(SourceUrl);
        }
    }
}
