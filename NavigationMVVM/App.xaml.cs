﻿using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System.Windows;

namespace NavigationMVVM;

public partial class App : Application
{
    private readonly AccountStore _accountStore;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _navigationStore = new NavigationStore();
        _accountStore = new AccountStore();
        _navigationBarViewModel = new NavigationBarViewModel(
            _accountStore,
            CreateHomeNavigationService(), 
            CreateAccountNavigationService(), 
            CreateLoginNavigationService()
            );
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        NavigationService<HomeViewModel> navigationService = CreateHomeNavigationService();
        navigationService.Navigate();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private NavigationService<HomeViewModel> CreateHomeNavigationService()
    {
        return new NavigationService<HomeViewModel>(
            _navigationStore, 
            () => new HomeViewModel(
                CreateLoginNavigationService()));
    }
    private NavigationService<LoginViewModel> CreateLoginNavigationService()
    {
        return new NavigationService<LoginViewModel>(
            _navigationStore, 
            () => new LoginViewModel(
                _accountStore,
                CreateAccountNavigationService()));
    }

    private NavigationService<AccountViewModel> CreateAccountNavigationService()
    {
        return new NavigationService<AccountViewModel>(
            _navigationStore, 
            () => new AccountViewModel(
                _accountStore,
                CreateHomeNavigationService()));
    }
}
