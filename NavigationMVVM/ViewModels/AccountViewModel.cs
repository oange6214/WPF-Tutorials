using NavigationMVVM.Commands;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class AccountViewModel : ViewModelBase
{
    public string Name => "BISP";

    public ICommand NavigateHomeCommand { get; }

    public AccountViewModel(NavigationStore navigationStore)
    {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(new Services.NavigationService<HomeViewModel>(
            navigationStore, () => new HomeViewModel(navigationStore)));
    }
}
 