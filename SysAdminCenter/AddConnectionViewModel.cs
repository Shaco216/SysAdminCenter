using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SysAdminCenter;

public class AddConnectionViewModel : ObservableRecipient
{
    private string _host = string.Empty;//= "dc01.example.local";
    private int port;//= 636;
    private bool useSsl;//= true;
    private string username = string.Empty;//= "user@example.com"; // oder use null + DefaultNetworkCredentials
    private string password = string.Empty;//= "P@ssw0rd";
    private string baseDn = string.Empty;//"DC=example,DC=local"
    private string filter = string.Empty;//"(objectClass=user)";

    public AddConnectionViewModel()
    {
        OnLoadCommand = new RelayCommand(OnLoad);
        OnDeleteCommand = new RelayCommand(OnDelete);
        SaveCommand = new RelayCommand(OnSave);
        CancelCommand = new RelayCommand(OnCancel);
    }

    public string Host
    {
        get { return _host; }
        set { SetProperty(ref _host, value); }
    }

    public int Port
    {
        get { return port; }
        set { SetProperty(ref port, value); }
    }

    public bool UseSsl
    {
        get { return useSsl; }
        set { SetProperty(ref useSsl, value); }
    }

    public string Username
    {
        get { return username; }
        set { SetProperty(ref username, value); }
    }

    public string Password
    {
        get { return password; }
        set { SetProperty(ref password, value); }
    }

    public string BaseDn
    {
        get { return baseDn; }
        set { SetProperty(ref baseDn, value); }
    }

    public string Filter
    {
        get { return filter; }
        set { SetProperty(ref filter, value); }
    }

    public IRelayCommand OnLoadCommand { get; }
    public IRelayCommand OnDeleteCommand { get; }
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }

    private void OnCancel()
    {
        throw new NotImplementedException();
    }

    private void OnSave()
    {
        throw new NotImplementedException();
    }

    private void OnDelete()
    {
        throw new NotImplementedException();
    }

    private void OnLoad()
    {
        throw new NotImplementedException();
    }
}
