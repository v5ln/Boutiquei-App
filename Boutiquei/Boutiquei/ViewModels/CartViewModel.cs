using System;
using Boutiquei.Services;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection <CartProduct> Cart { get; set; }
        public AppServices Services { get; set; }
        private int Total { get; set; }
        public String ShowTotal { get; set; }
        public CartViewModel(string UserID)
        {
            UserID = "User1";
            Cart = new ObservableCollection<CartProduct>();
            Cart = Services.GetCartProductsByUserID(UserID);
            Total = Services.GetCartTotalByUserID(UserID);
            ShowTotal = Total.ToString();
            //Cart.Add(new UserProduct { PID = "1", PName = "product 1", Price = "50", Quantity = 3, UserID = "50" , ProductImage= "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            //Cart.Add(new UserProduct { PID = "2", PName = "product 2", Price = "42", Quantity = 1, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111642554439/Screen_Shot_2022-04-03_at_9.29.41_PM.png" });
            //Cart.Add(new UserProduct { PID = "3", PName = "product 3", Price = "11", Quantity = 2, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111827087471/Screen_Shot_2022-04-03_at_9.29.29_PM.png" });
            //Cart.Add(new UserProduct { PID = "4", PName = "product 4", Price = "38", Quantity = 1, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            

        }
    }
}
