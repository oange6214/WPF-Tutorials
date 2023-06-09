using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System.Windows;

namespace NavigationMVVM.Commands;

public class LoginCommand : CommandBase
{
    private readonly LoginViewModel _viewModel;
    private readonly NavigationStore _navigationStore;

    public LoginCommand(LoginViewModel viewModel, NavigationStore navigationStore)
    {
        _viewModel = viewModel;
        _navigationStore = navigationStore;
    }

    public override void Execute(object? parameter)
    {
        MessageBox.Show($"Logging in {_viewModel.Username}");

        // Navigate to the account page.
        _navigationStore.CurrentViewModel = new AccountViewModel(_navigationStore);
    }
}
