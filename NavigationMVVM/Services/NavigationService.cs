using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System;

namespace NavigationMVVM.Services;

public class NavigationService<TViewModel>
    where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigateStore;
    private readonly Func<TViewModel> _createViewModel;

    public NavigationService(NavigationStore navigateStore, Func<TViewModel> createViewModel)
    {
        _navigateStore = navigateStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigateStore.CurrentViewModel = _createViewModel();
    }
}
