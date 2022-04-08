using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using System.Collections.Generic;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using System.Windows.Input;

namespace Boutiquei.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ObservableCollection<PImgs> ProductImages { get; set; }
        public ObservableCollection<Sizes> ProductSizes { get; set; }
        public ObservableCollection<Colors> ProductColores { get; set; }
        public CartProduct cartProduct { set; get; }
        private string _quantity;
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        AppServices Services = new AppServices();

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }


        public ProductViewModel(Product product)
        {
            Product = new Product();
            Product = product;
            ProductImages = new ObservableCollection<PImgs>();
            ProductSizes = new ObservableCollection<Sizes>();
            ProductColores = new ObservableCollection<Colors>();

            Quantity = "1";
            ProductImages = Services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = Services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = Services.GetAllBoutiqueProductColors(Product.BID, Product.PID);

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
        }

        public void onIncreaseTapped(object _product)
        {
            Quantity = (Convert.ToInt32(Quantity)+1).ToString();
        }

        public async void onDecreaseTapped(object _product)
        {
            if (Quantity.Equals("1"))
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's quantity must be higher than 1", "Ok");
                return;
            }
            Quantity = (Convert.ToInt32(Quantity) - 1).ToString();
        }


        // when add to cart btn 
        //public void AddToCart()
        //{
        //    cartProduct = new CartProduct({Quantity = Quantity  })
        //    services.AddToCart(cartProduct)
        //}


    }
}
