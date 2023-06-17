﻿using NavigationMVVM.ViewModels;
using System;

namespace NavigationMVVM.Stores;

public class ModalNavigationStore
{
    public event Action? CurrentViewModelChanged;

    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;

        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public bool IsOpen => CurrentViewModel != null;

    public void Close()
    {
        CurrentViewModel = null;
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}
