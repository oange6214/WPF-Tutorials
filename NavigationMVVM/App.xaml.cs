using Microsoft.Extensions.DependencyInjection;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System;
using System.Windows;

namespace NavigationMVVM;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<AccountStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();


        services.AddSingleton<INavigationService>(p => CreateHomeNavigationService(p));
        services.AddSingleton<CloseModalNavigationService>();


        services.AddTransient<HomeViewModel>(p => new HomeViewModel(CreateLoginNavigationService(p)));
        services.AddTransient<AccountViewModel>(p => new AccountViewModel(
            _serviceProvider.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(p)));
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
        services.AddSingleton<MainViewModel>();


        services.AddSingleton<MainWindow>(p => new MainWindow()
        {
            DataContext = p.GetRequiredService<MainViewModel>()
        });


        _serviceProvider = services.BuildServiceProvider();
    }

    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        CompositeNavigationService compositeNavigationService = new(
                serviceProvider.GetRequiredService<CloseModalNavigationService>(),
                CreateAccountNavigationService(serviceProvider)
            );

        return new LoginViewModel(serviceProvider.GetRequiredService<AccountStore>(), compositeNavigationService);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
        initialNavigationService.Navigate();

        MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            () => serviceProvider.GetRequiredService<HomeViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<LoginViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            () => serviceProvider.GetRequiredService<LoginViewModel>());
    }

    private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AccountViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            () => serviceProvider.GetRequiredService<AccountViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
    {
        return new NavigationBarViewModel(
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(serviceProvider),
            CreateAccountNavigationService(serviceProvider),
            CreateLoginNavigationService(serviceProvider)
            );
    }
}
