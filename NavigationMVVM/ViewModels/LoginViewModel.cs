﻿using NavigationMVVM.Commands;
using NavigationMVVM.Models;
using NavigationMVVM.Services;
using NavigationMVVM.Stores;
using System.Windows.Input;

namespace NavigationMVVM.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username;

    public string Username
    {
        get => _username;

        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password;

	public string Password
	{
		get => _password;

        set 
		{
			_password = value;
			OnPropertyChanged();
		}
	}

    public ICommand LoginCommand { get; }

    public LoginViewModel(AccountStore accountStore, NavigationStore navigationStore)
    {
        NavigationService<AccountViewModel> navigationService = new(
            navigationStore,
            () => new AccountViewModel(accountStore, navigationStore));

        LoginCommand = new LoginCommand(this, accountStore, navigationService);
    }

}
