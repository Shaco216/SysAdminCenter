using Microsoft.Extensions.DependencyInjection;

namespace SysAdminCenter;

public static class AppHost
{
    private static ServiceProvider? _serviceProvider;

    public static void Initialize()
    {
        var services = new ServiceCollection();

        // 🧩 ViewModels (ohne Parameter)
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<MainWindow>();

        // 🧩 Repositories
        //services.AddTransient<IKategorieRepository, KategorieRepository>();
        //services.AddTransient<IPersonRepository, PersonRepository>();

        // 🧩 WindowManager oder andere Services
        services.AddSingleton<IWindowManager, WindowManager>();

        // 🧩 ViewModel (wird per DI erstellt, benötigt IKategorieRepository)
        //services.AddTransient<KategorieViewModel>();
        //services.AddTransient<PersonViewModel>();

        // Optional: einfache Factory, die eine neue KategorieViewModel-Instanz aus dem Container liefert
        //services.AddTransient<Func<KategorieViewModel>>(provider => () => provider.GetRequiredService<KategorieViewModel>());
        //services.AddTransient<Func<PersonViewModel>>(provider => () => provider.GetRequiredService<PersonViewModel>());
        //services.AddTransient<Func<MainViewModel>>(provider => () => provider.GetRequiredService<MainViewModel>());

        _serviceProvider = services.BuildServiceProvider();
    }

    public static T GetService<T>() where T : notnull
        => _serviceProvider.GetRequiredService<T>();
}
