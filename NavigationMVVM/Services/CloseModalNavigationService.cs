using NavigationMVVM.Stores;
using System;

namespace NavigationMVVM.Services;

public class CloseModalNavigationService : INavigationService
{
    private readonly ModalNavigationStore _navigationStore;

    public CloseModalNavigationService(ModalNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void Navigate()
    {
        _navigationStore.Close();
    }
}
