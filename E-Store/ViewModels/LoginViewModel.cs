﻿using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows.Input;
namespace E_Store.ViewModels;

internal class LoginViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;

    public ObservableCollection<Member> Members;
    public Member CurrentUser { get; set; }

    public ICommand AboutCommand { get; set; }
    public ICommand HomeCommand { get; set; }
    public ICommand OurFruitCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }

    private string _username;

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            ErrorText = "";
            OnPropertyChanged(nameof(Username));
        }
    }

    private SecureString _password;

    public SecureString Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
            ErrorText = "";
            OnPropertyChanged(nameof(Password));
        }
    }

    private string _errortext;

    public string ErrorText
    {
        get { return _errortext; }
        set { _errortext = value; OnPropertyChanged(nameof(ErrorText)); }
    }


    public LoginViewModel(NavigationStore navigationStore, Member CurrentUser)
    {
        _navigationStore = navigationStore;
        this.CurrentUser = CurrentUser;
        Members = Database_Service.GetMembers();

        AboutCommand = new RelayCommand(ExecuteAboutCommand);
        HomeCommand = new RelayCommand(ExecuteHomeCommand);
        OurFruitCommand = new RelayCommand(ExecuteOurFruitCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        RegisterCommand = new RelayCommand(ExecuteRegisterCommand);
    }

    private void ExecuteAboutCommand(object? parametr) => _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, CurrentUser);
    private void ExecuteHomeCommand(object? parametr) => _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, CurrentUser);
    private void ExecuteOurFruitCommand(object? paremetr) => _navigationStore.CurrentViewModel = new OurFruitViewModel(_navigationStore, CurrentUser);
    private void ExecuteLoginCommand(object? parametr)
    {
        foreach (var member in Members)
        {
            if (member.UserName == Username && member.Password == Password.ToStringForSecureStr())
            {
                CurrentUser = member;
                if (member.IsAdmin)
                    _navigationStore.CurrentViewModel = new AdminPanelViewModel(_navigationStore, member);
                else
                    _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, CurrentUser);
                return;
            }
        }

        ErrorText = "Username Or Password Incorrect!";
    }
    private bool CanExecuteLoginCommand(object? obj)
    {
        bool validData;
        if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
            Password == null || Password.Length < 3)
            validData = false;
        else
            validData = true;

        return validData;
    }
    private void ExecuteRegisterCommand(object? obj) =>
        _navigationStore.CurrentViewModel = new RegisterViewModel(_navigationStore, CurrentUser);
}
