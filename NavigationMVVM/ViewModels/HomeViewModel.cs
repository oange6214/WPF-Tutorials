using NavigationMVVM.Commands;
using NavigationMVVM.Services;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public string WelcomeMessage => "Welcome to my app";

    public ICommand NavigateLoginCommand { get; }

    public HomeViewModel(NavigationService<LoginViewModel> loginNavigationService)
    {
        NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
    }
}
