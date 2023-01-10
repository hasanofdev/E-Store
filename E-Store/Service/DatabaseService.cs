using DevExpress.Mvvm.Native;
using E_Store.Models;
using E_Store.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

    public static List<Member> GetMembers()
    {
        var Users = new List<Member>();
        using (var database = new SQLiteDbContext())
        {
            database.Members.ForEach(p => Users.Add(p));
        }
        return Users;
    }
}
