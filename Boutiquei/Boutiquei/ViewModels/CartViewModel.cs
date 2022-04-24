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

        public AppServices Services;



        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CheckoutCommand { get; }

        public CartViewModel()
        {


            Services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            cartFromAPI = new ObservableCollection<CartProduct>();
            Total = "0";
            cartFromAPI = Services.GetCartProductsByUserID();

            cartFromAPI.CollectionChanged += CartChanged;

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
            CheckoutCommand = new Xamarin.Forms.Command(onCheckoutTapped);
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
            Total = await Services.GetTotalProductsPrice();
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
            await Services.UpdateIncreaseQuantity(product.PID);
            Total = await Services.GetTotalProductsPrice();
        }
        public async void onDecreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.UpdateDecreaseQuantity(product.PID);
            Total = await Services.GetTotalProductsPrice();

        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.DeleteFromCart(product.PID);
            Total = await Services.GetTotalProductsPrice();
        }
    }
}
