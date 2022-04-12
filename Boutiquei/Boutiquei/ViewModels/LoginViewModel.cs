using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
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
        public Command SubmitCommand { get; }

        IGoogleAuth auth;
        public LoginViewModel()
        {
            auth = DependencyService.Get<IGoogleAuth>();
            SubmitCommand = new Command(async () => await SignIn(email, password));
        }

        async Task SignIn(string email, string password)
        {
            string token = await auth.LoginWithEmailAndPassword(email, password);
            Console.WriteLine("token: "+token);
            if (token != string.Empty)
            {
                try
                {
                    await SecureStorage.SetAsync("oauth_token", token);
                    App.Current.MainPage = new AppShell();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

    }
}
