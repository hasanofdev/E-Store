using DevExpress.Mvvm.Native;
using E_Store.Models;
using E_Store.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace E_Store.Service;

public class Database_Service
{

    public static ObservableCollection<Product> GetProducts()
    {
        var products = new ObservableCollection<Product>();
        using(var database = new SQLiteDbContext())
        {
            database.Products.ForEach(p => products.Add(p));
        }
        return products;
    }

    public static void SaveProducts(ObservableCollection<Product> products)
    {
        using (var database = new SQLiteDbContext())
        {
            database.Products.ExecuteDelete();
            database.Products.AddRange(products.ToArray());
            database.SaveChanges();
        }
    }

    public static ObservableCollection<Member> GetMembers()
    {
        var Users = new ObservableCollection<Member>();
        using (var database = new SQLiteDbContext())
        {
            database.Members.ForEach(p => Users.Add(p));
        }
        return Users;
    }

    public static void SaveMembers(ObservableCollection<Member> members)
    {
        using (var database = new SQLiteDbContext())
        {
            database.Members.ExecuteDelete();
            database.Members.AddRange(members.ToArray());
            database.SaveChanges();
        }
    }
}
