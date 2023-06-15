﻿namespace NavigationMVVM.ViewModels;

public class LayoutViewModel
{

    public NavigationBarViewModel NavigationBarViewModel { get; }
    public ViewModelBase ContentViewModel { get; }

    public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel)
    {
        NavigationBarViewModel = navigationBarViewModel;
        ContentViewModel = contentViewModel;
    }

}