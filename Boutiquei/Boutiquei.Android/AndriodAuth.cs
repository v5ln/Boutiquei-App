using System;
using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndriodAuth))]
namespace Boutiquei.Droid
{
    public class AndriodAuth : IGoogleAuth
    {
        public bool IsSigIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;

        }

        public bool IsSigOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;


            }
            catch (Exception ex)
            {

                return false;

            }

        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                var token = user.User.Uid;

                return token.ToString();
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {


            try
            {

                var auth = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = auth.User.Uid;

                return token.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
