using System.Windows.Input;

namespace DesktopClient
{
    public sealed class ServiceStackExample : ObservableObject, IServiceExample
    {
        public string Header { get; } = "Service Stack Way";

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

        public ServiceStackExample()
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
