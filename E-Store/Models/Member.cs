using System.ComponentModel;

namespace E_Store.Models
{
    public class Member : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private bool isAdmin;

        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; OnPropertyChanged(nameof(IsAdmin)); }
        }

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(Id)); } }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(Name); }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(Surname); }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(UserName); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(Password); }
        }

        public Member() { }

        public Member(int id, string name, string surname, string username, string password, bool isAdmin)
        {
            Id = id;
            Name = name;
            Surname = surname;
            UserName = username;
            Password = password;
            this.isAdmin = isAdmin;
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

}
