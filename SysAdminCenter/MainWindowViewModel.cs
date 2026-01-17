using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace SysAdminCenter;

public class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        OpenConnectionCommand = new RelayCommand(OnOpenConnectionCommand);
        NewConnectionCommand = new RelayCommand(OnNewConnectionCommand);
    }

    private void OnNewConnectionCommand()
    {
        throw new NotImplementedException();
    }

    private void OnOpenConnectionCommand()
    {
        throw new NotImplementedException();
    }

    public IRelayCommand OpenConnectionCommand;
    public IRelayCommand NewConnectionCommand;
}
