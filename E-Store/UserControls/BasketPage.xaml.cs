using DevExpress.Mvvm.Native;
using E_Store.Command;
using E_Store.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace E_Store.UserControls
{
    /// <summary>
    /// Interaction logic for BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Window
    {
        public Member CurrentUser;
        public double TotalCost;
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

        public ICommand BuyAllCommand { get; set; }


        public ObservableCollection<Order> Orders { get; set; }

        public BasketPage(ObservableCollection<Order> orders, Member member)
        {
            DataContext = this;
            CurrentUser = member;
            Orders = orders;
            BuyAllCommand = new RelayCommand(ExecuteBuyAllCommand);
            InitializeComponent();
        }

        private void ExecuteBuyAllCommand(object? obj)
        {
            double price = default;
            Orders.ForEach(o=>price+= o.Price * o.Weight);
            MessageBoxResult result =  MessageBox.Show($"Total Cost - {price}$. Do You Want Continue?","Payment",MessageBoxButton.YesNo,MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    _notifier.ShowSuccess("Purchase Completed Successfully!");
                    break;
                default:
                    _notifier.ShowWarning("Payment Cancelled");
                    break;
            }
        }

    }
}
