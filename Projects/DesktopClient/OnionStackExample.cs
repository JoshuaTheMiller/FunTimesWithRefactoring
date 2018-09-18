namespace DesktopClient
{
    public sealed class OnionStackExample : ServiceExample
    {
        public override string Header { get; } = "Onion Example";

        private int output = 0;

        protected override void OnExecute(object obj)
        {
            this.output++;
            SearchOutput = output.ToString();            
        }
    }
}
