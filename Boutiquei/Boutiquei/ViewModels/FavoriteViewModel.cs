using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Favorite { get; set; }
        //public AsyncCommand<Product> DeleteCommand { get; }
        public AppServices Services;
        public FavoriteViewModel()
        {
            Services = new AppServices();
            Favorite = new ObservableCollection<Product>();
            Favorite = Services.GetFavouriteProductsByUserID("User1");
            //Favorite.Add(new CartProduct { PID = "1", PName = "product 1", Price = 50, Quantity = 3, BID = "50",  PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            //Favorite.Add(new CartProduct { PID = "2", PName = "product 2", Price = 42, Quantity = 1, BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111642554439/Screen_Shot_2022-04-03_at_9.29.41_PM.png" });
            //Favorite.Add(new CartProduct { PID = "3", PName = "product 3", Price = 11, Quantity = 2, BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111827087471/Screen_Shot_2022-04-03_at_9.29.29_PM.png" });
            //Favorite.Add(new CartProduct { PID = "4", PName = "product 4", Price = 38, Quantity = 1, BID = "50", PImgCover = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            //DeleteCommand = new AsyncCommand<Product>(Delete);


        }
        //async Task Delete(Product product)
        //{
        //    if (product == null)
        //    {
        //        await Shell.Current.DisplayAlert("Deleted", "Null", "OK");
        //        return;
        //    }

        //    Favorite.Remove(product);
        //    await Shell.Current.DisplayAlert("Deleted", product.PName, "OK");

        //}
    }
}
