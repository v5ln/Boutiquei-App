using System;
using Boutiquei.Services;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Boutiquei.Views;
using Xamarin.Essentials;

namespace Boutiquei.ViewModels
{
    public class CartViewModel : BaseViewModel
    {

        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


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
        public AppServices Services;

        

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CheckoutCommand { get; }

        public CartViewModel()
        {
            //string UserID = ;
            _ = AccessToken();

            Console.WriteLine(accessToken);


            Services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            Total = "0";
            Cart = Services.GetCartProductsByUserID();

            Cart.CollectionChanged += CartChanged;

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
            CheckoutCommand = new Xamarin.Forms.Command(onCheckoutTapped);
        }

        

        private async void CartChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0].GetType() != typeof(Boutiquei.Models.CartProduct))
                {
                    Console.WriteLine((string)e.NewItems[0]);
                    Cart.Clear();
                }
                else
                {
                    System.Threading.Thread.Sleep(800);
                    Total = await Services.GetTotalProductsPrice();
                }
            }
        }
        private async void onCheckoutTapped(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());
        }
        public async void onIncreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.UpdateIncreaseQuantity( product.PID);
            Total = await Services.GetTotalProductsPrice();
        }
        public async void onDecreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.UpdateDecreaseQuantity( product.PID);
            Total = await Services.GetTotalProductsPrice();
            
        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.DeleteFromCart( product.PID);
            Total = await Services.GetTotalProductsPrice();
        }
    }
}
