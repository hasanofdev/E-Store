using DevExpress.Mvvm.Native;
using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using E_Store.UserControls;
using E_Store.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace E_Store.ViewModels;

internal class OurFruitViewModel : BaseViewModel
{
    private ObservableCollection<Product> _products;
    public ObservableCollection<Product> Products { get; set;}

    public ICommand AboutCommand { get; set; }
    public ICommand HomeCommand { get; set; }
    public ICommand LoginCommand { get; set; }

    public Member CurrentUser { get; private set; }

    public Visibility LoginBtnVisibility { get; private set; }
    public Visibility ProfileBtnVisibility { get; private set; }

    private string _searchText;
    public string SearchText
    {
        get { return _searchText; }
        set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
    }

    private readonly NavigationStore _navigationStore;

    public OurFruitViewModel(NavigationStore navigationStore, Member currentUser)
    {
        Products = Database_Service.GetProducts();
        _navigationStore = navigationStore;
        CurrentUser = currentUser;
        AboutCommand = new RelayCommand(ExecuteAboutCommand);
        HomeCommand = new RelayCommand(ExecuteHomeCommand);
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

    private void ExecuteLoginCommand(object? parametr) => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, CurrentUser);
    private void ExecuteAboutCommand(object? parametr) => _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, CurrentUser);
    private void ExecuteHomeCommand(object? parametr) => _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, CurrentUser);
    
}
