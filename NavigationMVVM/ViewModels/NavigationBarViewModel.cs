using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class NavigationBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;

    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateAccountCommand { get; }
    public ICommand NavigateLoginCommand { get; }
    public ICommand NavigatePeopleListingCommand { get; }
    public ICommand LogoutCommand { get; }

    public bool IsLoggedIn => _accountStore.IsLoggedIn;

    public NavigationBarViewModel(
        AccountStore accountStore,
        INavigationService homeNavigationService,
        INavigationService accountNavigationService,
        INavigationService loginNavigationService,
        INavigationService peopleListingNavigationCommand)
    {
        _accountStore = accountStore;
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
        NavigateAccountCommand = new NavigateCommand(accountNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        NavigatePeopleListingCommand = new NavigateCommand(peopleListingNavigationCommand);

        LogoutCommand = new LogoutCommand(_accountStore);

        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
    }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(IsLoggedIn));
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }

}
