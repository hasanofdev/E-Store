using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Windows.Input;

namespace E_Store.ViewModels;

internal class RegisterViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;

    public ObservableCollection<Member> Members;
    public Member CurrentUser { get; set; }

    public ICommand AboutCommand { get; set; }
    public ICommand HomeCommand { get; set; }
    public ICommand OurFruitCommand { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }

    private string _name;
    private string _surname;
    private string _username;
    private SecureString _password;
    private string _resultMessage;
    private System.Windows.Media.Brush _brush;

    public System.Windows.Media.Brush ResultMessageColor
    {
        get { return _brush; }
        set { _brush = value; OnPropertyChanged(nameof(ResultMessageColor)); }
    }

    public string ResultMessage
    {
        get { return _resultMessage; }
        set
        {
            _resultMessage = value;
            OnPropertyChanged(nameof(ResultMessage));
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            ResultMessage = "";
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Surname
    {
        get { return _surname; }
        set
        {
            _surname = value;
            ResultMessage = "";
            OnPropertyChanged(nameof(Surname));
        }
    }

    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            ResultMessage = "";
            OnPropertyChanged(nameof(Username));
        }
    }

    public SecureString Password
    {
        get { return _password; }
        set
        {
            _password = value;
            ResultMessage = "";
            OnPropertyChanged(nameof(Password));
        }
    }

    public RegisterViewModel(NavigationStore navigationStore, Member currentUser)
    {
        _navigationStore = navigationStore;
        this.CurrentUser = currentUser;
        ResultMessageColor = System.Windows.Media.Brushes.Transparent;
        Members = Database_Service.GetMembers();

        AboutCommand = new RelayCommand(ExecuteAboutCommand);
        HomeCommand = new RelayCommand(ExecuteHomeCommand);
        OurFruitCommand = new RelayCommand(ExecuteOurFruitCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand);
        RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
    }

    private void ExecuteAboutCommand(object? parametr) => _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, CurrentUser);
    private void ExecuteHomeCommand(object? parametr) => _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, CurrentUser);
    private void ExecuteOurFruitCommand(object? paremetr) => _navigationStore.CurrentViewModel = new OurFruitViewModel(_navigationStore, CurrentUser);
    private void ExecuteLoginCommand(object? parametr) => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, CurrentUser);

    private void ExecuteRegisterCommand(object? parametr)
    {
        foreach (var member in Members.ToList())
        {
            if (member.UserName == Username)
            {
                ResultMessage = "Username is Already Exists!";
                ResultMessageColor = System.Windows.Media.Brushes.Red;
                return;
            }
            else
            {
                int id = Members[Members.Count - 1].Id + 1;
                Members.Add(new Member(id, Name, Surname, Username, Password.ToStringForSecureStr(), false));
                Database_Service.SaveMembers(Members);

                ResultMessage = "Thank you for registering. Welcome!";
                ResultMessageColor = System.Windows.Media.Brushes.Green;
            }

        }
    }

    private bool CanExecuteRegisterCommand(object? parametr)
    {
        bool result = true;
        if (
            string.IsNullOrWhiteSpace(Name)
            || Name.Length <= 3
            || string.IsNullOrWhiteSpace(Surname)
            || Surname.Length <= 3
            || string.IsNullOrWhiteSpace(Username)
            || Username.Length <= 3
            || string.IsNullOrWhiteSpace(Password.ToStringForSecureStr())
            || Password.Length <= 3
            )
            result = false;
        return result;
    }
}
