using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class AccountViewModel : ViewModelBase
{
    private AccountStore _accountStore;

    public string Username => _accountStore.CurrentAccount?.Username;

    public string Email => _accountStore.CurrentAccount?.Email;

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(AccountStore accountStore, NavigationService<HomeViewModel> homeNavigationService)
    {
        _accountStore = accountStore;

        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
    }
}
 