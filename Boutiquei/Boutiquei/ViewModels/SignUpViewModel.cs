using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {

        IGoogleAuth auth;
        public ICommand SigUpCommad { get; }
        public SignUpViewModel()
        {
            auth = DependencyService.Get<IGoogleAuth>();
            SigUpCommad = new Command(async () => await SignUp(email, password));
        }
        

        
        

        public string email { get; set; }
        public string password { get; set; }

        private async Task SignUp(string email, string password)
        {
            try
            {
                string ulr = await auth.SignUpWithEmailAndPassword(email, password);
                Console.WriteLine(ulr);



            }
            catch (Exception ex)
            {
                Console.WriteLine("The Exceptions : " + ex);
            }
        }
    }
}
