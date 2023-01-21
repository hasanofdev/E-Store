using DevExpress.Mvvm.Native;
using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using E_Store.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace E_Store.ViewModels;

internal class OurFruitViewModel : BaseViewModel
{
    public ObservableCollection<Product> Products { get; set; }
    public ObservableCollection<Order> NewOrders { get; set; }
    public ObservableCollection<Order> AllOrders { get; set; }

    public ICommand AboutCommand { get; set; }
    public ICommand HomeCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand BasketCommand { get; set; }
    public ICommand BuyNowCommand { get; set; }

    public Member CurrentUser { get; private set; }
    public Visibility LoginBtnVisibility { get; private set; }
    public Visibility ProfileBtnVisibility { get; private set; }
    private readonly Notifier _notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 10,
            offsetY: 10);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(5));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private readonly NavigationStore _navigationStore;

    public OurFruitViewModel(NavigationStore navigationStore, Member currentUser)
    {
        Products = Database_Service.GetProducts();
        _navigationStore = navigationStore;
        CurrentUser = currentUser;
        AllOrders = Database_Service.GetOrders();
        NewOrders = new();


        AboutCommand = new RelayCommand(ExecuteAboutCommand);
        HomeCommand = new RelayCommand(ExecuteHomeCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand);
        BasketCommand = new RelayCommand(ExecuteBasketCommand);
        BuyNowCommand = new RelayCommand(ExecuteBuyNowCommand);

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

    private void ExecuteBuyNowCommand(object? obj)
    {
        ProductControl product = obj as ProductControl;
        if (!string.IsNullOrWhiteSpace(CurrentUser.Name) && NewOrders.IndexOf(p => p.ProductName == product.ProductName) == -1)
        {
            NewOrders.Add(new Order(CurrentUser.UserName, product.ProductName, product.Price, 0));
            _notifier.ShowSuccess($"{product.ProductName} Added\nMove To Basket To See All");
        }
        else if (NewOrders.IndexOf(p => p.ProductName == product.ProductName) != -1)
            _notifier.ShowError(product.ProductName + " Already Exists!");
        else if (string.IsNullOrWhiteSpace(CurrentUser.Name))
            _notifier.ShowInformation("Please Login or Create New Account.");

    }

    private void ExecuteLoginCommand(object? parametr) => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, CurrentUser);
    private void ExecuteAboutCommand(object? parametr) => _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, CurrentUser);
    private void ExecuteHomeCommand(object? parametr) => _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, CurrentUser);
    private void ExecuteBasketCommand(object? parametr)
    {
        if (string.IsNullOrWhiteSpace(CurrentUser.Name))
            ExecuteLoginCommand(null);
        else
        {
            BasketPage page = new(NewOrders, CurrentUser,AllOrders);
            page.ShowDialog();
        }
    }

}
