using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System.Windows;

namespace NavigationMVVM;

public partial class App : Application
{
    private readonly AccountStore _accountStore;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _navigationStore = new NavigationStore();
        _accountStore = new AccountStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        INavigationService<HomeViewModel> navigationService = CreateHomeNavigationService();
        navigationService.Navigate();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private INavigationService<HomeViewModel> CreateHomeNavigationService()
    {
        return new LayoutNavigationService<HomeViewModel>(
            _navigationStore, 
            () => new HomeViewModel(CreateLoginNavigationService()),
            CreateNavigationBarViewModel);
    }
    private INavigationService<LoginViewModel> CreateLoginNavigationService()
    {
        return new NavigationService<LoginViewModel>(
            _navigationStore, 
            () => new LoginViewModel(_accountStore, CreateAccountNavigationService()));
    }

    private INavigationService<AccountViewModel> CreateAccountNavigationService()
    {
        return new LayoutNavigationService<AccountViewModel>(
            _navigationStore, 
            () => new AccountViewModel(_accountStore, CreateHomeNavigationService()),
            CreateNavigationBarViewModel);
    }

    private NavigationBarViewModel CreateNavigationBarViewModel()
    {
        return new NavigationBarViewModel(
            _accountStore,
            CreateHomeNavigationService(),
            CreateAccountNavigationService(),
            CreateLoginNavigationService()
            );
    }
}
