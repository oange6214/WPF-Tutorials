using NavigationMVVM.Commands;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public ICommand NavigateAccountCommand { get; }

    public HomeViewModel(NavigationStore navigationStore)
    {
        NavigateAccountCommand = new NavigateCommand<AccountViewModel>(navigationStore, () => new AccountViewModel(navigationStore));
    }
}
