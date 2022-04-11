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
        private ObservableCollection<Product> favorite;
        public ObservableCollection<Product> Favorite {
            get
            {
                return favorite;
            }
            set
            {
                favorite = value;
                OnPropertyChanged();
            }

        }
        
        public AppServices Services;

        public ICommand DeleteCommand { get; }
        public FavoriteViewModel()
        {
            Services = new AppServices();
            Favorite = new ObservableCollection<Product>();
            Favorite = Services.GetFavouriteProductsByUserID("User1");
            Favorite.CollectionChanged += Favorite_CollectionChanged;
            
            
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
        }

        private void Favorite_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0].ToString() != "Boutiquei.Models.Product")
                {
                    Favorite.Clear();
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                OnPropertyChanged();
            }

        }

        public async void onDeleteTapped(object _product)
        {
            var product = _product as Product;
            await Services.DeleteFromFavourites("User1", product.PID);
        }
    }
}
