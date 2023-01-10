using Microsoft.Extensions.Configuration;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
