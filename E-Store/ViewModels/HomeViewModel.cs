using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace E_Store.ViewModels;

internal class HomeViewModel : BaseViewModel
{
    public Member CurrentUser { get; set; }
    private readonly NavigationStore _navigationStore;
    public List<Product> Products { get; set; }
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

    #region MessageFields

    private string _email;
    private string _fullname;
    private string _message;

    public string Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string FullName
    {
        get { return _fullname; }
        set { _fullname = value; OnPropertyChanged(nameof(FullName)); }
    }

    public string Message
    {
        get { return _message; }
        set { _message = value; OnPropertyChanged(nameof(Message)); }
    }

    #endregion


    public ICommand AboutCommand { get; set; }
    public ICommand OurFruitCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand NavigateCommand { get; set; }
    public ICommand SendMessageCommand { get; set; }

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
        SendMessageCommand = new RelayCommand(ExecuteSendMessageCommand,CanExecteSendMessageCommand);


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
    private void ExecuteSendMessageCommand(object? parametr)
    {
        try
        {
            MailAddress m = new MailAddress(Email);
        }
        catch (FormatException ex)
        {
            _notifier.ShowError(ex.Message);
            return;
        }

        var Messages = Database_Service.GetMessages();
        int id;

        if (Messages.Count == 0)
            id = 1;
        else
            id = Messages.Count + 1;

        Database_Service.SaveMessages(new MessageData(id,Message,Email,FullName));
        _notifier.ShowSuccess("Your message has been sent successfully!");
    }
    private bool CanExecteSendMessageCommand(object? obj)
    {
        bool validData = true;
        if (string.IsNullOrWhiteSpace(Message) || Message.Length < 3
            || string.IsNullOrWhiteSpace(Email) || Email.Length < 3
            || string.IsNullOrWhiteSpace(FullName) || FullName.Length < 3)
            validData = false;

        return validData;
    }
}
