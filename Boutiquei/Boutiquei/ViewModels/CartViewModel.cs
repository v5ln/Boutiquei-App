using System;
using Boutiquei.Services;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Specialized;
using Boutiquei.Views;
using Plugin.Connectivity;

namespace Boutiquei.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private string _total;
        public string Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CartProduct> cart;
        public ObservableCollection<CartProduct> Cart
        {
            get
            {
                return cart;
            }
            set
            {
                cart = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CartProduct> cartFromAPI { get; set; }

        private readonly AppServices services;



        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CheckoutCommand { get; }

        public CartViewModel()
        {


            services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            cartFromAPI = new ObservableCollection<CartProduct>();
            Total = "0";
            cartFromAPI = services.GetCartProductsByUserID();

            cartFromAPI.CollectionChanged += CartChanged;

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
            CheckoutCommand = new Xamarin.Forms.Command(onCheckoutTapped);

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
                    cartFromAPI.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    cartFromAPI.Clear();
                }
            };
        }


        private void CartChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.Action);
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] != null)
                {
                    Cart = new ObservableCollection<CartProduct>(cartFromAPI);
                    UpdateTotal();
                    OnPropertyChanged();
                }
                else
                {
                    OnPropertyChanged();
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Cart = new ObservableCollection<CartProduct>(cartFromAPI);
                UpdateTotal();
                OnPropertyChanged();
            }

        }
        private async void UpdateTotal()
        {
            System.Threading.Thread.Sleep(800);
            Total = await services.GetTotalProductsPrice();
        }
        private async void onCheckoutTapped(object obj)
        {
           
            if (Total != "0")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());
            }
        
            else
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please add products to cart", "Ok");
            }
    
        }
        
        public async void onIncreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await services.UpdateIncreaseQuantity(product.PID);
            Total = await services.GetTotalProductsPrice();
        }
        public async void onDecreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await services.UpdateDecreaseQuantity(product.PID);
            Total = await services.GetTotalProductsPrice();

        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as CartProduct;
            await services.DeleteFromCart(product.PID);
            Total = await services.GetTotalProductsPrice();
        }

       
    }
}
