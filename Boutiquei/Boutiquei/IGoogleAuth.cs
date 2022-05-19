using System;
using System.Threading.Tasks;

namespace Boutiquei
{
    public interface IGoogleAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        
        bool IsSigIn();
        bool IsSigOut();

        void SignOut();
    }
}
