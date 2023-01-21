using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace E_Store.Models;

internal class MessageData : INotifyPropertyChanged
{
    private int _id;
    private string _message;
    private string _email;
    private string _fullname;
    private DateTime _created;

    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(nameof(Id)); }
    }

    public string Message
    {
        get { return _message; }
        set { _message = value; OnPropertyChanged(nameof(Message)); }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string Fullname
    {
        get { return _fullname; }
        set { _fullname = value;OnPropertyChanged(Fullname); }
    }

    public DateTime Created
    {
        get { return _created; }
        set { _created = value; OnPropertyChanged(nameof(Created)); }
    }

    public MessageData(int id,string message,string email,string fullname)
    {
        Id = id;
        Message = message;
        Email = email;
        Fullname = fullname;
        Created = DateTime.Now;
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
