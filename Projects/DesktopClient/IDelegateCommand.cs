using System.Windows.Input;

namespace DesktopClient
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
