using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace E_Store.UserControls;

/// <summary>
/// Interaction logic for ProductInBasketControl.xaml
/// </summary>
public partial class ProductInBasketControl : UserControl, INotifyPropertyChanged
{
    public double Weight
    {
        get { return (double)GetValue(WeightProperty); }
        set { SetValue(WeightProperty, value); OnPropertyChanged(nameof(WeightProperty)); }
    }

    public static readonly DependencyProperty WeightProperty =
        DependencyProperty.Register("Weight", typeof(double), typeof(ProductInBasketControl));

    public string ProductName
    {
        get { return (string)GetValue(ProductNameProperty); }
        set { SetValue(ProductNameProperty, value); }
    }

    public static readonly DependencyProperty ProductNameProperty =
      DependencyProperty.Register("ProductName", typeof(string), typeof(ProductInBasketControl));


    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    public ProductInBasketControl()
    {
        InitializeComponent();
    }
}
