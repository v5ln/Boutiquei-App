using System;
using System.Threading.Tasks;
using Boutiquei.iOS;
using Firebase.Auth;
using Foundation;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSAuth))]
namespace Boutiquei.iOS
{
    public class IOSAuth : IGoogleAuth
    {
        public bool IsSigIn()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public void SignOut()
        {
            Auth.DefaultInstance.SignOut(out NSError error);
        }

        public bool IsSigOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return user.User.Uid;
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);

                return user.User.Uid;
            
        }

    }
}
