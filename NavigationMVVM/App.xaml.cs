﻿using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using NavigationMVVM.ViewModels;
using System.Windows;

namespace NavigationMVVM;

public partial class App : Application
{
    private readonly AccountStore _accountStore;
    private readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    public App()
    {
        _accountStore = new AccountStore();
        _navigationStore = new NavigationStore();
        _modalNavigationStore = new ModalNavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        INavigationService navigationService = CreateHomeNavigationService();
        navigationService.Navigate();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore, _modalNavigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private INavigationService CreateHomeNavigationService()
    {
        return new LayoutNavigationService<HomeViewModel>(
            _navigationStore, 
            () => new HomeViewModel(CreateLoginNavigationService()),
            CreateNavigationBarViewModel);
    }
    private INavigationService CreateLoginNavigationService()
    {
        return new ModalNavigationService<LoginViewModel>(
            _modalNavigationStore, 
            () => new LoginViewModel(_accountStore, CreateAccountNavigationService()));
    }

    private INavigationService CreateAccountNavigationService()
    {
        return new LayoutNavigationService<AccountViewModel>(
            _navigationStore, 
            () => new AccountViewModel(_accountStore, CreateHomeNavigationService()),
            CreateNavigationBarViewModel);
    }

    private NavigationBarViewModel CreateNavigationBarViewModel()
    {
        return new NavigationBarViewModel(
            _accountStore,
            CreateHomeNavigationService(),
            CreateAccountNavigationService(),
            CreateLoginNavigationService()
            );
    }
}
