using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace E_Store.ViewModels;

internal class HomeViewModel : BaseViewModel
{
    public Member CurrentUser { get; set; }
    private readonly NavigationStore _navigationStore;

    public List<Product> Products { get; set; }

    public ICommand AboutCommand { get; set; }
    public ICommand OurFruitCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand NavigateCommand { get; set; }

    public Visibility LoginBtnVisibility { get; private set; }
    public Visibility ProfileBtnVisibility { get; private set; }

    public HomeViewModel(NavigationStore navigationStore, Member currentUser)
    {
        Products = new();
        int i = 0;
        foreach (var item in Database_Service.GetProducts().ToList())
        {
            if (i++ == 6)
                break;
            Products.Add(item);
        }
        CurrentUser = currentUser;
        _navigationStore = navigationStore;


        AboutCommand = new RelayCommand(ExecuteAboutCommand);
        OurFruitCommand = new RelayCommand(ExecuteOurFruitCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand);
        NavigateCommand = new RelayCommand(ExecuteNavigateCommand);


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

    private void ExecuteLoginCommand(object? parametr) => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, CurrentUser);
    private void ExecuteOurFruitCommand(object? parametr) => _navigationStore.CurrentViewModel = new OurFruitViewModel(_navigationStore, CurrentUser);
    private void ExecuteAboutCommand(object? parametr) => _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, CurrentUser);
    private void ExecuteNavigateCommand(object? parametr) => Process.Start("chrome.exe", "https://www.google.com/maps/place/STEP+Komp%C3%BCter+Akademiyas%C4%B1,+Baku/@40.4149839,49.8510875,17z/data=!3m1!4b1!4m5!3m4!1s0x403087fbef6b3dfb:0xc32b1d5765759234!8m2!3d40.4149839!4d49.8532762");
}
