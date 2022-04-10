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
using System.Collections.Specialized;

namespace Boutiquei.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ObservableCollection<PImgs> ProductImages { get; set; }
        public ObservableCollection<Sizes> ProductSizes { get; set; }
        public ObservableCollection<Colors> ProductColores { get; set; }
        ObservableCollection<Product> ProductsInFav { get; set; }
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
        private string _FavBtn;
        public string FavBtn
        {
            get
            {
                return _FavBtn;
            }
            set
            {
                _FavBtn = value;
                OnPropertyChanged();
            }
        }

        AppServices Services = new AppServices();

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand FavouriteCommand { get; }


        public ProductViewModel(Product product)
        {
            Product = new Product();
            Product = product;
            ProductImages = new ObservableCollection<PImgs>();
            ProductSizes = new ObservableCollection<Sizes>();
            ProductColores = new ObservableCollection<Colors>();
            ProductsInFav = new ObservableCollection<Product>();
            Quantity = "1";

            ProductImages = Services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = Services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = Services.GetAllBoutiqueProductColors(Product.BID, Product.PID);
            FavBtn = "FAR";
            //ProductsInFav = Services.GetFavouriteProductsByUserID("User1");
            //ProductsInFav.CollectionChanged += isInFavouriteListChanged;

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            FavouriteCommand = new Xamarin.Forms.Command(onFavouriteTapped);
        }

        //private void isInFavouriteListChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    Application.Current.MainPage.DisplayAlert("x", "PEPE ", "x");
        //    Quantity = "91341";
        //    ObservableCollection<Product> products = (ObservableCollection<Product>)sender;
        //    if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (var product in products)
        //        {
        //            if (product.PID == Product.PID)
        //            {
        //                FavBtn = "FAS";
        //            }
        //        }
        //        FavBtn = "FAR";
        //    }
        //}

        private async void onFavouriteTapped()
        {

            if(FavBtn == "FAR")
            {
                FavBtn = "FAS";
                await Application.Current.MainPage.DisplayAlert("x", Product.PID, "x");
                await Services.AddToFavourites(Product, "User1");
            }
            else
            {
                FavBtn = "FAR";
                await Services.DeleteFromFavourites("User1", Product.PID);
            }

        }

        private void onIncreaseTapped(object _product)
        {
            Quantity = (Convert.ToInt32(Quantity)+1).ToString();
        }

        private async void onDecreaseTapped(object _product)
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
