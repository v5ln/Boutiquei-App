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

namespace Boutiquei.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<CartProduct> Cart { get; set; }
        public AppServices Services;

        private string _total;
        public string Total {
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

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand DeleteCommand { get; }
        public CartViewModel()
        {
            //string UserID = "User1";
            Services = new AppServices();
            Cart = new ObservableCollection<CartProduct>();
            Cart = Services.GetCartProductsByUserID("User1");
            Total = Services.GetTotalProductsPrice("User1");

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            DeleteCommand = new Xamarin.Forms.Command(onDeleteTapped);
        }
        public async void onIncreaseTapped(object _product)
        {
            var product = _product as Product;
            await Services.UpdateIncreaseQuantity("User1", product.PID);
            Total = Services.GetTotalProductsPrice("User1");
        }
        public async void onDecreaseTapped(object _product)
        {
            var product = _product as Product;
            await Services.UpdateDecreaseQuantity("User1", product.PID);
            Total = Services.GetTotalProductsPrice("User1");
        }
        public async void onDeleteTapped(object _product)
        {
            var product = _product as Product;
            await Services.DeleteFromCart("User1", product.PID);
            Total = Services.GetTotalProductsPrice("User1");
        }
    }
}
