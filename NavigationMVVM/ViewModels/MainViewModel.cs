using NavigationMVVM.Stores;
using NavigationMVVM.ValidationRules;
using System;

namespace NavigationMVVM.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

    public bool IsModalOepn => _modalNavigationStore.IsOpen;

    public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
        _navigationStore = navigationStore;
        _modalNavigationStore = modalNavigationStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    private void OnCurrentModalViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentModalViewModel));
        OnPropertyChanged(nameof(IsModalOepn));
    }

    private string _myProperty;
    public string MyProperty
    {
        get => _myProperty;
        set
        {
            if (_myProperty != value)
            {
                _myProperty = value;
                this.ValidateProperty(() => MyProperty, new NotEmptyValidationRule());
                OnPropertyChanged();
            }
        }
    }
}
