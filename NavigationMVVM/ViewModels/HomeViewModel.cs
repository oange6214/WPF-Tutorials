using NavigationMVVM.Commands;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public string WelcomeMessage => "Welcome to my app";

    public ICommand NavigateLoginCommand { get; }

    public HomeViewModel(NavigationStore navigationStore)
    {
        NavigateLoginCommand = new NavigateCommand<LoginViewModel>(new Services.NavigationService<LoginViewModel>(
            navigationStore, () => new LoginViewModel(navigationStore)));
    }
}
