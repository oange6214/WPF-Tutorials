using NavigationMVVM.Commands;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public string WelcomeMessage => "Welcome to my app";

    public ICommand NavigateAccountCommand { get; }

    public HomeViewModel(NavigationStore navigationStore)
    {
        NavigateAccountCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
    }
}
