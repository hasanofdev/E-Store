using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace E_Store.Models;

public class Product : INotifyPropertyChanged
{
    private int _id;

    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(nameof(Id)); }
    }

    public string _productName;


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
        set { _imageUrl = value; OnPropertyChanged(ImageUrl); }
    }

    public Product() { }

    public Product(int id, string productName, double price, string imageUrl)
    {
        Id = id;
        ProductName = productName;
        Price = price;
        ImageUrl = imageUrl;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler handler = this.PropertyChanged;
        if (handler != null)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
    }

}
