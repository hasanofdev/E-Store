using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace E_Store.ViewModels;

internal class AboutViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;

    public ICommand HomeCommand { get; set; }
    public ICommand OurFruitCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public Member CurrentUser { get; private set; }
    public Visibility LoginBtnVisibility { get; private set; }
    public Visibility ProfileBtnVisibility { get; private set; }

    public AboutViewModel(NavigationStore navigationStore,Member currentUser)
    {
        _navigationStore = navigationStore;
        CurrentUser = currentUser;
        HomeCommand = new RelayCommand(ExecuteHomeCommand);
        OurFruitCommand = new RelayCommand(ExecuteFruitCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand);

        if (string.IsNullOrWhiteSpace(CurrentUser.Name))
        {
            LoginBtnVisibility = Visibility.Visible;
            ProfileBtnVisibility = Visibility.Hidden;
        }
        else
        {
            LoginBtnVisibility = Visibility.Hidden;
            ProfileBtnVisibility = Visibility.Visible;
        }
    }

    private void ExecuteLoginCommand(object? obj) => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore,CurrentUser);
    private void ExecuteHomeCommand(object? obj) => _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore,CurrentUser);
    private void ExecuteFruitCommand(object? obj) => _navigationStore.CurrentViewModel = new OurFruitViewModel(_navigationStore,CurrentUser);
}
