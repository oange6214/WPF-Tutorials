using NavigationMVVM.Models;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;

namespace NavigationMVVM.Commands;

public class LoginCommand : CommandBase
{
    private readonly LoginViewModel _viewModel;
    private readonly AccountStore _accountStore;
    private readonly INavigationService _navigationService;

    public LoginCommand(
        LoginViewModel viewModel, 
        AccountStore accountStore,
        INavigationService navigationService)
    {
        _viewModel = viewModel;
        _accountStore = accountStore;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        Account account = new Account
        {
            Email = $"{_viewModel.Username}@test.com",
            Username = _viewModel.Username,
        };

        _accountStore.CurrentAccount = account;
        _navigationService.Navigate();
    }
}
