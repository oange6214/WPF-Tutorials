using NavigationMVVM.Models;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;

namespace NavigationMVVM.Commands;

public class LoginCommand : CommandBase
{
    private readonly LoginViewModel _viewModel;
    private readonly ParameterNavigationService<Account, AccountViewModel> _navigationService;
    private readonly NavigationStore _navigationStore;

    public LoginCommand(LoginViewModel viewModel, ParameterNavigationService<Account, AccountViewModel> navigationService)
    {
        _viewModel = viewModel;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        Account account = new Account
        {
            Email = $"{_viewModel.Username}@test.com",
            Username = _viewModel.Username,
        };

        _navigationStore.CurrentViewModel = new AccountViewModel(account, _navigationStore);
    }
}
