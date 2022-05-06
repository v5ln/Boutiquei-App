using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Specialized;
using Plugin.Connectivity;
using Boutiquei.ViewModels;


[assembly: Xamarin.Forms.Dependency(typeof(ProductViewModel))]

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

        private readonly AppServices services = new AppServices();

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

            ProductImages = services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = services.GetAllBoutiqueProductColors(Product.BID, Product.PID);
            //FavBtn = "FAR";

            productsInFav = services.GetFavouriteProductsByUserID();
            productsInCart = services.GetCartProductsByUserID();
            productsInFav.CollectionChanged += productsInFavListChanged;
            productsInCart.CollectionChanged += productsInCartListChanged;

            IncreaseCommand = new Xamarin.Forms.Command(OnIncreaseTapped);
            DecreaseCommand = new Xamarin.Forms.Command(OnDecreaseTapped);
            FavouriteCommand = new Xamarin.Forms.Command(OnFavouriteTapped);
            CartCommand = new Xamarin.Forms.Command(OnCartTapped);

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
                    ProductImages.Clear();
                    ProductSizes.Clear();
                    ProductColores.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    ProductImages.Clear();
                    ProductSizes.Clear();
                    ProductColores.Clear();
                   
                }
            };
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

        private async void OnCartTapped(object obj)
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
            await services.AddToCart(cartProduct);
           
            await Application.Current.MainPage.DisplayAlert("Message", "Product added to the cart successfully", "Ok");
        }

        private async void OnFavouriteTapped()
        {

            if (FavBtn == "FAR")
            {
                FavBtn = "FAS";
                await services.AddToFavourites(Product);
                _ = Application.Current.MainPage.DisplayAlert("Message", "Prodact added to Favourites successfully", "Ok");

            }
            else
            {
                FavBtn = "FAR";
                await services.DeleteFromFavourites( Product.PID);
            }

        }

        private void OnIncreaseTapped(object _product)
        {
            Quantity = (Convert.ToInt32(Quantity)+1).ToString();
        }

        private async void OnDecreaseTapped(object _product)
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
