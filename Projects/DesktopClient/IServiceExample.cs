using System.Windows.Input;

namespace DesktopClient
{
    public interface IServiceExample
    {
        string Header { get; }
        string SearchInput { get; set; }
        string SearchOutput { get; }
        IDelegateCommand ExecuteCommand { get; }
        void SetHostUrl(string sourceUrl);
    }
}
