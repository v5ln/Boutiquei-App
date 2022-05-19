using System.Collections.ObjectModel;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        private ObservableCollection<Product> favorite;
        public ObservableCollection<Product> Favorite
        {
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

        private readonly AppServices services;

        public ICommand DeleteCommand { get; }
        public FavoriteViewModel()
        {
            services = new AppServices();
            Favorite = new ObservableCollection<Product>();
            Favorite = services.GetFavouriteProductsByUserID();
            Favorite.CollectionChanged += Favorite_CollectionChanged;


            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
            ChickWifiOnStart();
            ChickWifiContinuously();
        }

        private bool _imgIsVisible;

        public bool ImgIsVisible
        {
            get => _imgIsVisible;
            set
            {
                _imgIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _contentIsVisible;

        public bool ContentIsVisible
        {
            get => _contentIsVisible;
            set
            {
                _contentIsVisible = value;
                OnPropertyChanged();
            }
        }

        private string _connection;

        public string Connection
        {
            get => _connection;
            set
            {
                _connection = value;
                OnPropertyChanged();
            }
        }

        public void ChickWifiOnStart()
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                ContentIsVisible = true;
                ImgIsVisible = false;
            }
            else
            {
                Connection = "Nointernetconnection.png";
                ContentIsVisible = false;
                ImgIsVisible = true;
            }
        }
        public void ChickWifiContinuously()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {

                if (args.IsConnected)
                {

                    ContentIsVisible = true;
                    ImgIsVisible = false;
                    Favorite.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    Favorite.Clear();
                }
            };
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
            await services.DeleteFromFavourites(product.PID);
        }


        private Product previousSelected;
        Product selectedProduct;
        string typeOfStore { set; get; }
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                 
                    if (value != null)
                    {

                        if (value.BID[0] == 'B')
                        {
                            typeOfStore = "Boutique";
                        }
                        else
                        {
                            typeOfStore = "Brand";
                        }

                        Application.Current.MainPage.Navigation.PushAsync(new ProductPage(value, typeOfStore));


                        previousSelected = value;

                        value = null;
                    }
                    selectedProduct = value;
                    OnPropertyChanged();

               
            }
        }
    }
}
