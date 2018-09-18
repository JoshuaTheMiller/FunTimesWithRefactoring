using System.Windows.Input;

namespace DesktopClient
{
    public sealed class OnionStackExample : ObservableObject, IServiceExample
    {
        public string Header { get; } = "Onion Example";

        private string searchInput = string.Empty;
        public string SearchInput
        {
            get => searchInput;
            set => Set(ref searchInput, value);
        }

        private int output = 0;
        private string searchOutput = string.Empty;
        public string SearchOutput
        {
            get => searchOutput;
            set => Set(ref searchOutput, value);
        }

        public ICommand ExecuteCommand { get; }

        public OnionStackExample()
        {
            ExecuteCommand = new DelegateCommand(OnExecute);
        }

        private void OnExecute(object obj)
        {
            this.output++;
            SearchOutput = output.ToString();
        }
    }
}
