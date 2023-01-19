using Autofac;
using E_Store.Models;
using E_Store.Navigations;
using E_Store.ViewModels;
using System.Windows;
using IContainer = Autofac.IContainer;

namespace E_Store;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IContainer? Container { get; private set; }

    private void ApplicationStartup(object sender, StartupEventArgs e)
    {

        NavigationStore navigationStore = new();
        Member CurrentUser = new();
        var builder = new ContainerBuilder();

        builder.RegisterInstance(navigationStore).SingleInstance();
        builder.RegisterInstance(CurrentUser).SingleInstance();

        builder.RegisterType<MainViewModel>();
        builder.RegisterType<HomeViewModel>();
        builder.RegisterType<AboutViewModel>();
        builder.RegisterType<OurFruitViewModel>();
        builder.RegisterType<LoginViewModel>();

        Container = builder.Build();

        navigationStore.CurrentViewModel = Container.Resolve<HomeViewModel>();
        MainView mainView = new();

        mainView.DataContext = Container.Resolve<MainViewModel>();

        mainView.Show();

    }

}
