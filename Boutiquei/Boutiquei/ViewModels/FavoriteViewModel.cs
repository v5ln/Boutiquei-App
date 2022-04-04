using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        public ObservableCollection<UserProduct> Favorite { get; set; }
        public AsyncCommand<UserProduct> DeleteCommand { get; }
        public FavoriteViewModel()
        {
            Favorite = new ObservableCollection<UserProduct>();
            Favorite.Add(new UserProduct { PID = "1", PName = "product 1", Price = "50", Quantity = 3, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            Favorite.Add(new UserProduct { PID = "2", PName = "product 2", Price = "42", Quantity = 1, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111642554439/Screen_Shot_2022-04-03_at_9.29.41_PM.png" });
            Favorite.Add(new UserProduct { PID = "3", PName = "product 3", Price = "11", Quantity = 2, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111827087471/Screen_Shot_2022-04-03_at_9.29.29_PM.png" });
            Favorite.Add(new UserProduct { PID = "4", PName = "product 4", Price = "38", Quantity = 1, UserID = "50", ProductImage = "https://cdn.discordapp.com/attachments/959590694392053820/960245111445401630/Screen_Shot_2022-04-03_at_9.30.55_PM.png" });
            DeleteCommand = new AsyncCommand<UserProduct>(Delete);


        }
        async Task Delete(UserProduct product)
        {
            if (product == null)
            {
                await Shell.Current.DisplayAlert("Deleted", "Null", "OK");
                return;
            }

            Favorite.Remove(product);
            await Shell.Current.DisplayAlert("Deleted", product.PName, "OK");

        }
    }
}
