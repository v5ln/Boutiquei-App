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
        public AppServices Services;

        

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CheckoutCommand { get; }
        public CartViewModel()
        {
            //string UserID = "User1";
            Services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            Total = "0";
            Cart = Services.GetCartProductsByUserID("User1");

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
                    Total = await Services.GetTotalProductsPrice("User1");
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
            await Services.UpdateIncreaseQuantity("User1", product.PID);
            Total = await Services.GetTotalProductsPrice("User1");
        }
        public async void onDecreaseTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.UpdateDecreaseQuantity("User1", product.PID);
            Total = await Services.GetTotalProductsPrice("User1");
            
        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as CartProduct;
            await Services.DeleteFromCart("User1", product.PID);
            Total = await Services.GetTotalProductsPrice("User1");
        }
    }
}
