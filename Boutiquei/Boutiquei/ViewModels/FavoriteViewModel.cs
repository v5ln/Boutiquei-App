using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public AppServices Services;

        public ICommand DeleteCommand { get; }
        public FavoriteViewModel()
        {
            Services = new AppServices();
            Favorite = new ObservableCollection<Product>();
            Favorite = Services.GetFavouriteProductsByUserID("User1");
            
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as Product;
            await Services.DeleteFromFavourites("User1", product.PID);
        }
    }
}
