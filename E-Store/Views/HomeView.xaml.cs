using Microsoft.Extensions.Configuration;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;

namespace E_Store.Views;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["MapKey"]!;
        map.CredentialsProvider = new ApplicationIdCredentialsProvider(key);
    }


}
