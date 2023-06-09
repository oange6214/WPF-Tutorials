using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;

namespace NavigationMVVM.Commands;

public class NavigateHomeCommand : CommandBase
{
    private readonly NavigationStore _navigateStore;

    public NavigateHomeCommand(NavigationStore navigateStore)
    {
        _navigateStore = navigateStore;
    }

    public override void Execute(object? parameter)
    {
        _navigateStore.CurrentViewModel = new HomeViewModel();
    }
}
