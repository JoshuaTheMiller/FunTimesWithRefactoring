namespace DesktopClient
{
    public abstract class ServiceExample : ObservableObject, IServiceExample
    {
        protected string SourceUrl { get; private set; }

        public abstract string Header { get; }

        private string searchInput = string.Empty;
        public string SearchInput
        {
            get => searchInput;
            set => Set(ref searchInput, value);
        }
        
        private string searchOutput = string.Empty;
        public string SearchOutput
        {
            get => searchOutput;
            protected set => Set(ref searchOutput, value);
        }

        public IDelegateCommand ExecuteCommand { get; }

        protected ServiceExample()
        {
            ExecuteCommand = new DelegateCommand(OnExecute, CanExecute);
        }

        private bool CanExecute(object arg)
        {
            if (string.IsNullOrWhiteSpace(SourceUrl))
            {
                return false;
            }

            return true;
        }

        protected abstract void OnExecute(object obj);

        public void SetHostUrl(string sourceUrl)
        {
            this.SourceUrl = sourceUrl;

            ExecuteCommand.RaiseCanExecuteChanged();
        }
    }
}
