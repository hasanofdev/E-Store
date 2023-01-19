using E_Store.Command;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.Service;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace E_Store.ViewModels
{
    internal class AdminPanelViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Product> Products { get; set; }
        public Product SelectedItem { get; set; }

        public ICommand ExitCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ChooseFileCommand { get; set; }

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

        public Member currentUser { get; private set; }

        public AdminPanelViewModel(NavigationStore navigationStore, Member currentUser)
        {
            _navigationStore = navigationStore;
            this.currentUser = currentUser;
            Products = Database_Service.GetProducts();

            ExitCommand = new RelayCommand(ExecuteExitCommand);
            EditCommand = new RelayCommand(ExecuteEditCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteTextBoxChangeCommand);
            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteTextBoxChangeCommand);
            ChooseFileCommand = new RelayCommand(ExecuteChooseFileCommand);
        }

        private void ExecuteUpdateCommand(object? obj)
        {
            if (!ImageUrlExists(ImageUrl))
            {
                MessageBox.Show("Image Url Incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedItem is null)
            {
                MessageBox.Show("Item not selected!");
                return;
            }

            SelectedItem.ProductName = ProductName;
            SelectedItem.Price = Price;
            SelectedItem.ImageUrl = ImageUrl;
        }

        private void ExecuteExitCommand(object? obj)
        {
            currentUser = new();
            Database_Service.SaveProducts(Products);
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, currentUser);
        }

        private void ExecuteEditCommand(object? obj)
        {
            ProductName = SelectedItem.ProductName;
            Price = SelectedItem.Price;
            ImageUrl = SelectedItem.ImageUrl;
        }

        private void ExecuteDeleteCommand(object? obj) =>
            Products.Remove(SelectedItem);

        private void ExecuteAddCommand(object? obj)
        {
            int id = Products[Products.Count - 1].Id + 1;
            if (!ImageUrlExists(ImageUrl))
            {
                MessageBox.Show("Image Url Incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Products.Add(new Product(id, ProductName, Price, ImageUrl));
        }

        private void ExecuteChooseFileCommand(object? obj)
        {
            OpenFileDialog openFile = new();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            openFile.ShowDialog();
            ImageUrl = openFile.FileName;
        }

        private bool CanExecuteTextBoxChangeCommand(object? obj)
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
    }
}
