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
        private string _imageUrl;
        private bool isAdmin;

        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; OnPropertyChanged(nameof(IsAdmin)); }
        }

        public int Id { get; set; }

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

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(ImageUrl); }
        }

        public Member() { }

        public Member(int id, string name, string surname, string username, string password, string imageUrl, bool isAdmin)
        {
            Id = id;
            Name = name;
            Surname = surname;
            UserName = username;
            Password = password;
            ImageUrl = imageUrl;
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
