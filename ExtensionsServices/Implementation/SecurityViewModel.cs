using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using Command.Implementation;
using Security.Implementation;

namespace ExtensionsServices.Implementation
{
    public class SecurityViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private RelayCommand _loginCommand;

        public SecurityViewModel()
        {
            _username = SecurityService.AdminUserName;
            _password = "";
            _loginCommand = new RelayCommand(DoLogin, CanLogin);
        }

        public virtual Visibility LoginVisible
        {
            get { return ServiceProvider.Security.UseLogin ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                ServiceProvider.Security.CurrentUserName = _username;
                OnPropertyChanged();
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Status
        {
            get { return ServiceProvider.Security.CurrentUserName; }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public void DoLogin()
        {
            OnPropertyChanged(nameof(Status));
        }

        public bool CanLogin()
        {
            return ServiceProvider.Security.CheckPassword(_username, _password);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}