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
        services.AddSingleton<PeopleStore>();
        services.AddSingleton<NavigationStore>();
        services.AddSingleton<ModalNavigationStore>();


        services.AddSingleton<INavigationService>(p => CreateHomeNavigationService(p));
        services.AddSingleton<CloseModalNavigationService>();


        services.AddTransient<HomeViewModel>(p => new HomeViewModel(CreateLoginNavigationService(p)));
        services.AddTransient<AccountViewModel>(p => new AccountViewModel(
            _serviceProvider.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(p)));
        services.AddTransient<LoginViewModel>(CreateLoginViewModel);
        services.AddTransient<PeopleListingViewModel>(p => new PeopleListingViewModel(
            p.GetRequiredService<PeopleStore>(),
            CreateAddPersonNavigationService(p)));
        services.AddTransient<AddPersonViewModel>(p => new AddPersonViewModel(
            p.GetRequiredService<PeopleStore>(),
            p.GetRequiredService<CloseModalNavigationService>()
            ));
        services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);
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

    private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
    {
        CompositeNavigationService compositeNavigationService = new(
                serviceProvider.GetRequiredService<CloseModalNavigationService>(),
                CreateAccountNavigationService(serviceProvider)
            );

        return new LoginViewModel(serviceProvider.GetRequiredService<AccountStore>(), compositeNavigationService);
    }

    private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<HomeViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(), 
            () => serviceProvider.GetRequiredService<HomeViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }
    private INavigationService CreateAddPersonNavigationService(IServiceProvider serviceProvider)
    {
        return new ModalNavigationService<AddPersonViewModel>(
            serviceProvider.GetRequiredService<ModalNavigationStore>(),
            () => serviceProvider.GetRequiredService<AddPersonViewModel>());
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

    private INavigationService CreatePeopleListingNavigationService(IServiceProvider serviceProvider)
    {
        return new LayoutNavigationService<PeopleListingViewModel>(
            serviceProvider.GetRequiredService<NavigationStore>(),
            () => serviceProvider.GetRequiredService<PeopleListingViewModel>(),
            () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
    }

    private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
    {
        return new NavigationBarViewModel(
            serviceProvider.GetRequiredService<AccountStore>(),
            CreateHomeNavigationService(serviceProvider),
            CreateAccountNavigationService(serviceProvider),
            CreateLoginNavigationService(serviceProvider),
            CreatePeopleListingNavigationService(serviceProvider)
            );
    }

}
