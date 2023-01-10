using System;
using System.Windows;
using System.Windows.Controls;

namespace E_Store.UserControls
{
    /// <summary>
    /// Interaction logic for ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {

        public event EventHandler<EventArgs>? BuyNowCommand;

        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }



        public static readonly DependencyProperty ProductNameProperty =
          DependencyProperty.Register("ProductName", typeof(string), typeof(ProductControl));

        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty =
          DependencyProperty.Register("Price", typeof(double), typeof(ProductControl));

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        public static readonly DependencyProperty ImageUrlProperty =
          DependencyProperty.Register("ImageUrl", typeof(string), typeof(ProductControl));

        public double WieghtWeightInstock { get; set; }


        public ProductControl()
        {
            InitializeComponent();
        }

        private void BuyNow_Click(object sender, RoutedEventArgs e)
        {
            BuyNowCommand?.Invoke(this, EventArgs.Empty);
        }
    }
}
