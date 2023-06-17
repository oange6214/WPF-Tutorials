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

        services.AddSingleton<INavigationService>(p => CreateHomeNavigationService(_serviceProvider));

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>(p => new MainWindow()
        {
            DataContext = p.GetRequiredService<MainViewModel>()
        });

        _serviceProvider = services.BuildServiceProvider();
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
            () => new HomeViewModel(CreateLoginNavigationService(serviceProvider)),
            () => CreateNavigationBarViewModel(serviceProvider));
    }

    private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        ModalNavigationStore modalNavigationStore = serviceProvider.GetRequiredService<ModalNavigationStore>();
        AccountStore accountStore = serviceProvider.GetRequiredService<AccountStore>();

        CompositeNavigationService compositeNavigationService = new (
                new CloseModalNavigationService(modalNavigationStore),
                CreateAccountNavigationService(serviceProvider)
            );

        return new ModalNavigationService<LoginViewModel>(
            modalNavigationStore, 
            () => new LoginViewModel(accountStore, compositeNavigationService ));
    }

    private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<AccountViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            () => new AccountViewModel(serviceProvider.GetRequiredService<AccountStore>(), CreateHomeNavigationService(serviceProvider)),
            () => CreateNavigationBarViewModel(serviceProvider));
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
