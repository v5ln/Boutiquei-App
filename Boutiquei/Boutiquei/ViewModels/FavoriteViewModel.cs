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
        private ObservableCollection<Product> favoriteFromApi{ get; set; }
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
            favoriteFromApi = new ObservableCollection<Product>();
            favoriteFromApi = Services.GetFavouriteProductsByUserID();
            favoriteFromApi.CollectionChanged += Favorite_CollectionChanged;
            
            
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
        }

        private void Favorite_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] != null)
                {
                    Favorite.Add((Product)e.NewItems[0]);
                    _ = Application.Current.MainPage.DisplayAlert("Message", "Prodact added to favorites successfully", "Ok");
                    OnPropertyChanged();
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Favorite.Remove((Product)e.OldItems[0]);
                OnPropertyChanged();
            }

        }

        public async void onDeleteTapped(object _product)
        {
            var product = _product as Product;
            await Services.DeleteFromFavourites(product.PID);
        }
    }
}
