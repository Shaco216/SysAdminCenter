namespace SysAdminCenter;

public interface IWindowManager
{
    void ShowWindow(object viewModel);
    bool? ShowDialog(object viewModel);
}
