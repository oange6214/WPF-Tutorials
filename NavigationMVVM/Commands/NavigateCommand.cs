using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System;

namespace NavigationMVVM.Commands;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigateStore;
    private readonly Func<TViewModel> _createViewModel;

    public NavigateCommand(NavigationStore navigateStore, Func<TViewModel> createViewModel)
    {
        _navigateStore = navigateStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
        _navigateStore.CurrentViewModel = _createViewModel();
    }
}
