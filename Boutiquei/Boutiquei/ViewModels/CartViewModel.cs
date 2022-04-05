using System;
using Boutiquei.Services;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;

namespace Boutiquei.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<CartProduct> Cart { get; set; }
        public AppServices Services;
        public string Total { get; set; }
        public CartViewModel()
        {
            //string UserID = "User1";
            Services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            Cart = Services.GetCartProductsByUserID("User1");
            Total = Services.GetCartTotalByUserID("User1");
            //Cart.Add(new CartProduct { PID = "1", PName = "product 1", Price = "50", Quantity = "3",  BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            //Cart.Add(new CartProduct { PID = "2", PName = "product 2", Price = "42", Quantity = "1", BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111642554439/Screen_Shot_2022-04-03_at_9.29.41_PM.png" });
            //Cart.Add(new CartProduct { PID = "3", PName = "product 3", Price = "11", Quantity = "2", BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111827087471/Screen_Shot_2022-04-03_at_9.29.29_PM.png" });
            //Cart.Add(new CartProduct { PID = "4", PName = "product 4", Price = "38", Quantity = "1", BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });


        }

    }
}
