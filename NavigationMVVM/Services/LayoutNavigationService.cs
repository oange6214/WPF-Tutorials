using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System;

namespace NavigationMVVM.Services;

public class LayoutNavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;
    private readonly NavigationBarViewModel _navigationBarViewModel;

    public LayoutNavigationService(
        NavigationStore navigateStore, 
        Func<TViewModel> createViewModel,
        NavigationBarViewModel navigationBarViewModel)
    {
        _navigationStore = navigateStore;
        _createViewModel = createViewModel;
        _navigationBarViewModel = navigationBarViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = new LayoutViewModel(_navigationBarViewModel, _createViewModel());
    }
}
