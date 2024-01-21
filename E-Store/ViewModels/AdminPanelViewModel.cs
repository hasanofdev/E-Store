using DevExpress.Mvvm.ModuleInjection.Native;
using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Input;

namespace E_Store.ViewModels
{

    internal class AdminPanelViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Member> Members { get; set; }
        public Product SelectedProduct { get; set; }
        public Member SelectedMember { get; set; }
        public Dictionary<string, bool> IsAdminChoices { get; set; }

        #region Visibilities
        private Visibility _membersVisibility;
        private Visibility _homeVisibility;
        private Visibility _walletVisibility;

        public Visibility MembersVisibility
        {
            get { return _membersVisibility; }
            set { _membersVisibility = value; OnPropertyChanged(nameof(MembersVisibility)); }
        }
        public Visibility HomeVisibility
        {
            get { return _homeVisibility; }
            set { _homeVisibility = value; OnPropertyChanged(nameof(HomeVisibility)); }
        }   
        public Visibility WalletVisibility
        {
            get { return _walletVisibility; }
            set { _walletVisibility = value; OnPropertyChanged(nameof(WalletVisibility)); }
        }


        #endregion

        #region HomeCommandDefine

        public ICommand ExitCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand HomeEditCommand { get; set; }
        public ICommand HomeDeleteCommand { get; set; }
        public ICommand HomeAddCommand { get; set; }
        public ICommand HomeUpdateCommand { get; set; }
        public ICommand HomeChooseFileCommand { get; set; }

        #endregion

        #region MembersCommandDefine

        public ICommand MembersCommand { get; set; }
        public ICommand MembersEditCommand { get; set; }
        public ICommand MembersUpdateCommand { get; set; }
        public ICommand MembersDeleteCommand { get; set; }
        public ICommand MembersAddCommand { get; set; }

        #endregion

        #region WalletCommandDefine

        public ICommand WalletCommand { get; set; }

        #endregion

        #region ProductsFields

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }

        #endregion

        #region MembersFields

        private int _id;
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private bool _auth;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public bool Auth
        {
            get { return _auth; }
            set { _auth = value; OnPropertyChanged(nameof(Auth)); }
        }

        #endregion

        public Member currentUser { get; private set; }

        public AdminPanelViewModel(NavigationStore navigationStore, Member currentUser)
        {
            _navigationStore = navigationStore;
            this.currentUser = currentUser;
            Products = Database_Service.GetProducts();
            Members = Database_Service.GetMembers();

            IsAdminChoices = new();
            IsAdminChoices.Add("Admin", true);
            IsAdminChoices.Add("Member", false);

            #region HomeCommandInit

            ExitCommand = new RelayCommand(ExecuteHomeExitCommand);
            HomeCommand = new RelayCommand(ExecuteHomeCommand);
            HomeEditCommand = new RelayCommand(ExecuteHomeEditCommand);
            HomeDeleteCommand = new RelayCommand(ExecuteHomeDeleteCommand);
            HomeAddCommand = new RelayCommand(ExecuteHomeAddCommand, CanExecuteHomeTextBoxChangeCommand);
            HomeUpdateCommand = new RelayCommand(ExecuteHomeUpdateCommand, CanExecuteHomeTextBoxChangeCommand);
            HomeChooseFileCommand = new RelayCommand(ExecuteHomeChooseFileCommand);

            #endregion

            #region MembersCommandInit

            MembersCommand = new RelayCommand(ExecuteMembersCommand);
            MembersEditCommand = new RelayCommand(ExecuteMembersEditCommand);
            MembersUpdateCommand = new RelayCommand(ExecuteMembersUpdateCommand,CanExecuteMembersAddCommand);
            MembersDeleteCommand = new RelayCommand(ExecuteMembersDeleteCommand);
            MembersAddCommand = new RelayCommand(ExecuteMembersAddCommand,CanExecuteMembersAddCommand);

            #endregion

            #region WalletCommandInit

            WalletCommand = new RelayCommand(ExecuteWalletCommand);

            #endregion

            MembersVisibility = Visibility.Collapsed;
            WalletVisibility = Visibility.Collapsed;
            HomeVisibility = Visibility.Visible;
        }

        #region HomeCommands

        private void ExecuteHomeCommand(object? obj)
        {
            Id = 0;
            Name = "";
            Surname = "";
            Username = "";
            Password = "";
            Auth = false;

            MembersVisibility = Visibility.Collapsed;
            WalletVisibility = Visibility.Collapsed;
            HomeVisibility = Visibility.Visible;
        }

        private void ExecuteHomeUpdateCommand(object? obj)
        {
            if (!ImageUrlExists(ImageUrl))
            {
                MessageBox.Show("Image Url Incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedProduct is null)
            {
                MessageBox.Show("Item not selected!");
                return;
            }

            SelectedProduct.ProductName = ProductName;
            SelectedProduct.Price = Price;
            SelectedProduct.ImageUrl = ImageUrl;
            MessageBox.Show("Succesfuly Updated!", "Product Uptaded", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteHomeExitCommand(object? obj)
        {
            currentUser = new();
            Database_Service.SaveProducts(Products);
            Database_Service.SaveMembers(Members);
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, currentUser);
        }

        private void ExecuteHomeEditCommand(object? obj)
        {

            if (SelectedProduct is null)
                return;

            ProductName = SelectedProduct.ProductName;
            Price = SelectedProduct.Price;
            ImageUrl = SelectedProduct.ImageUrl;
        }

        private void ExecuteHomeDeleteCommand(object? obj) =>
            Products.Remove(SelectedProduct);

        private void ExecuteHomeAddCommand(object? obj)
        {
            int id = Products[Products.Count - 1].Id + 1;
            if (!ImageUrlExists(ImageUrl))
            {
                MessageBox.Show("Image Url Incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Products.Add(new Product(id, ProductName, Price, ImageUrl));
            MessageBox.Show("Succesfuly Added!", "Product Added", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteHomeChooseFileCommand(object? obj)
        {
            OpenFileDialog openFile = new();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            openFile.ShowDialog();
            ImageUrl = openFile.FileName;
        }

        private bool CanExecuteHomeTextBoxChangeCommand(object? obj)
        {
            bool validData = false;

            if (string.IsNullOrWhiteSpace(ProductName) || ProductName.Length < 3 || Price <= 0)
                validData = false;
            else validData = true;

            return validData;
        }

        private static void GetPictureSize(string url, ref float width, ref float height, ref string err)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            System.IO.Stream mystream;
            System.Drawing.Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (System.Net.HttpWebRequest)WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new System.Drawing.Bitmap(mystream);
            }
            catch (Exception er)
            {
                err = er.Message;
                return;
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }
            width = bmp.Width;
            height = bmp.Height;
        }

        private static bool ImageUrlExists(string url)
        {

            float width = 0;
            float height = 0;
            string err = null;
            if (string.IsNullOrWhiteSpace(url))
                return width > 0;

            if (url.StartsWith("https://"))
                GetPictureSize(url, ref width, ref height, ref err);
            else
                ImageLocalUrlExists(url, ref width, ref height, ref err);
            return width > 0;
        }

        private static void ImageLocalUrlExists(string url, ref float width, ref float height, ref string err)
        {

            System.Drawing.Bitmap bmp;
            bmp = null;

            try
            {

                if (!string.IsNullOrWhiteSpace(url))
                    bmp = new System.Drawing.Bitmap(url);
            }
            catch (Exception er)
            {
                err = er.Message;
                return;
            }
            width = bmp.Width;
            height = bmp.Height;
        }

        #endregion

        #region MembersCommand

        private void ExecuteMembersCommand(object? obj)
        {
            ProductName = "";
            ImageUrl = "";
            Price= 0;

            MembersVisibility = Visibility.Visible;
            WalletVisibility = Visibility.Collapsed;
            HomeVisibility = Visibility.Collapsed;
        }

        private void ExecuteMembersEditCommand(object? obj)
        {
            if (SelectedMember is null)
                return;
            
            Id = SelectedMember.Id;
            Name = SelectedMember.Name;
            Surname = SelectedMember.Surname;
            Username = SelectedMember.UserName;
            Password = SelectedMember.Password;
            Auth = SelectedMember.IsAdmin;
        }

        private void ExecuteMembersUpdateCommand(object? obj)
        {
            if (SelectedMember is null)
            {
                MessageBox.Show("Member not selected!");
                return;
            }

            SelectedMember.Name = Name;
            SelectedMember.Surname = Surname;
            SelectedMember.UserName = Username;
            SelectedMember.Password = Password;
            SelectedMember.IsAdmin = Auth;
            MessageBox.Show("Succesfuly Uptaded!", "Member Uptaded", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteMembersDeleteCommand(object? obj)
        {
            Members.Remove(SelectedMember);
        }

        private void ExecuteMembersAddCommand(object? obj)
        {
            int id = Members[Members.Count - 1].Id + 1;
            if (Members.Any(m => m.UserName == Username))
            {
                MessageBox.Show("Username Already Exists!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            Members.Add(new Member(id, Name, Surname, Username, Password, Auth));
            MessageBox.Show("Succesfuly Added!","Member Added",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private bool CanExecuteMembersAddCommand(object? obj)
        {
            bool validData = false;

            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3
                || string.IsNullOrWhiteSpace(Surname) || Surname.Length < 3
                || string.IsNullOrWhiteSpace(Username) || Username.Length < 3
                || string.IsNullOrWhiteSpace(Password) || Password.Length < 3)
                validData = false;
            else validData = true;

            return validData;
        }

        #endregion

        #region WalletCommand

        private void ExecuteWalletCommand (object? obj)
        {

            MembersVisibility = Visibility.Collapsed;
            WalletVisibility = Visibility.Visible;
            HomeVisibility = Visibility.Collapsed;
        }

        #endregion
    }
}
