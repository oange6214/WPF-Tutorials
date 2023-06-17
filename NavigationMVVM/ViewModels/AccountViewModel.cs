using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class AccountViewModel : ViewModelBase
{
    private AccountStore _accountStore;

    public string? Username => _accountStore.CurrentAccount?.Username;
    public string? Email => _accountStore.CurrentAccount?.Email;

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
    {
        _accountStore = accountStore;
        _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }

    private void OnCurrentAccountChanged()
    {
        OnPropertyChanged(nameof(Username));
        OnPropertyChanged(nameof(Email));
    }

    public override void Dispose()
    {
        _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
        base.Dispose();
    }
}
 