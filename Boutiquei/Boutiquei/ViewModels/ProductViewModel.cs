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
        private ObservableCollection<Product> productsInFav { get; set; }
        private ObservableCollection<CartProduct> productsInCart { get; set; }
        private CartProduct cartProduct { set; get; }

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

        private string _size;
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
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

        private bool isInCart = false;

        AppServices Services = new AppServices();

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand FavouriteCommand { get; }
        public ICommand CartCommand { get; }


        public ProductViewModel(Product product)
        {
            Product = new Product();
            Product = product;
            ProductImages = new ObservableCollection<PImgs>();
            ProductSizes = new ObservableCollection<Sizes>();
            ProductColores = new ObservableCollection<Colors>();
            productsInFav = new ObservableCollection<Product>();
            Quantity = "1";

            ProductImages = Services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = Services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = Services.GetAllBoutiqueProductColors(Product.BID, Product.PID);
            //FavBtn = "FAR";

            productsInFav = Services.GetFavouriteProductsByUserID("User1");
            productsInCart = Services.GetCartProductsByUserID("User1");
            productsInFav.CollectionChanged += productsInFavListChanged;
            productsInCart.CollectionChanged += productsInCartListChanged;

            IncreaseCommand = new Xamarin.Forms.Command(onIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(onDecreaseTapped);
            FavouriteCommand = new Xamarin.Forms.Command(onFavouriteTapped);
            CartCommand = new Xamarin.Forms.Command(onCartTapped);

             

        }

       
        private void productsInCartListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<CartProduct> products = (ObservableCollection<CartProduct>)sender;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0].ToString() != "Boutiquei.Models.CartProduct")
                {
                    productsInCart.Clear();
                }
                else
                {
                    foreach (var product in products)
                    {
                        if (product.PID == Product.PID)
                        {
                            isInCart = true;
                            return;
                        }
                    }
                    isInCart = false;
                }
            }
        }

        private void productsInFavListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FavBtn = "FAR";
            
            ObservableCollection<Product> products = (ObservableCollection<Product>)sender;
            
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0].ToString() != "Boutiquei.Models.Product")
                {
                    productsInFav.Clear();
                }
                else
                {
                    foreach (var product in products)
                    {
                        if (product.PID == Product.PID)
                        {
                            FavBtn = "FAS";
                            return;
                        }
                    }
                }
            }
        }

        private async void onCartTapped(object obj)
        {
            if (isInCart)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product already in cart", "Ok");
                return;
            }

            cartProduct = new CartProduct
            {
                BID = Product.BID,
                PID = Product.PID,
                PImgCover = Product.PImgCover,
                Quantity = Quantity,
                PName = Product.PName,
                Price = Product.Price,
                PColor = selectedColor,
                PSize = selectedSize
            };
            isInCart = true;
            await Services.AddToCart(cartProduct, "User1");

        }

        private async void onFavouriteTapped()
        {

            if (FavBtn == "FAR")
            {
                FavBtn = "FAS";
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


        //set and get sellected color : 
        public string selectedColor { set; get; }

        public void SetSelectedColor(string selectedColor)
        {
            this.selectedColor = selectedColor;
        }

        private Colors previousSelected;
        Colors _selectedColor;
        public Colors SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (value != null)
                {
                    SetSelectedColor(value.PColor);
                    //Application.Current.MainPage.DisplayAlert("selected color", value.PColor, "Ok");

                    previousSelected = value;

                    value = null;
                }
                _selectedColor = value;

            }
        }

        //set and get selected size from picker in vm size 
        public string selectedSize { set; get; }

        public void SetSelectedSize(string selectedSize)
        {
            this.selectedSize = selectedSize;
        }

        private Sizes previousSelected_;
        Sizes selectedSize_;
        public Sizes SelectedSize
        {
            get => selectedSize_;
            set
            {
                if (value != null)
                {
                    SetSelectedSize(value.PSize);
                    //Application.Current.MainPage.DisplayAlert("selected size", value.PSize, "Ok");
                    previousSelected_ = value;

                    value = null;
                }
                selectedSize_ = value;

            }
        }


        // when add to cart btn 
        //public void AddToCart()
        //{
        //    cartProduct = new CartProduct({Quantity = Quantity  })
        //    services.AddToCart(cartProduct)
        //}


    }
}
