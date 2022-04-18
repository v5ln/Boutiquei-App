using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class LoginViewModel 
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command LoginCommand { get; }
        public ICommand SignUpCommmand { get; }

        IGoogleAuth auth;
        public LoginViewModel()
        {
            auth = DependencyService.Get<IGoogleAuth>();
            LoginCommand = new Command(OnLoginTapped);
            SignUpCommmand = new Command(OnSignUpTapped);
        }

        private void OnSignUpTapped(object obj)
        {
            Application.Current.MainPage = new SignUpPage();
        }

        private async void OnLoginTapped(object obj)
        {
            if (Email == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the email", "Ok");
                return;
            }
            if (Password == "")
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You should write the email", "Ok");
                return;
            }
            string token = await auth.LoginWithEmailAndPassword(Email, Password);
            //Console.WriteLine("token: "+token);
            if (token != string.Empty)
            {
                try
                {
                    await SecureStorage.SetAsync("oauth_token", token);
                    Application.Current.MainPage = new AppShell();
                }
                catch 
                {
                    await Application.Current.MainPage.DisplayAlert("Faild", "Something went wrong, Please Try Again", "Ok");
                    Application.Current.MainPage = new LoginPage();
                }

            }
        }

    }
}