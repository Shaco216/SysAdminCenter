using System.Windows;

namespace SysAdminCenter;

public class WindowManager : IWindowManager
{
    public void ShowWindow(object viewModel)
    {
        var window = CreateWindowForViewModel(viewModel);
        window.Show();
    }

    public bool? ShowDialog(object viewModel)
    {
        var window = CreateWindowForViewModel(viewModel);
        return window.ShowDialog();
    }

    private Window CreateWindowForViewModel(object viewModel)
    {
        var viewModelType = viewModel.GetType();
        var viewTypeName = viewModelType.FullName!.Replace("ViewModel", "View");
        var viewAssembly = viewModelType.Assembly;

        var viewType = viewAssembly.GetType(viewTypeName);
        if (viewType == null)
            throw new InvalidOperationException($"Cannot find view for {viewModelType.FullName}");

        if (Activator.CreateInstance(viewType) is not Window window)
            throw new InvalidOperationException($"View {viewType.FullName} is not a Window.");

        window.DataContext = viewModel;
        return window;
    }
}
