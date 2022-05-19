using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.ViewModels;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using static System.Net.Mime.MediaTypeNames;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(SingleBrandViewModel))]
 
namespace Boutiquei.ViewModels
{
    public class SingleBrandViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Store Brand { get; set; }
        private readonly AppServices services;
        public SingleBrandViewModel(Store brand)
        {
            Products = new ObservableCollection<Product>();
            Brand = new Store();
            this.Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            services = new AppServices();
            Products = services.GetAllBrandProducts(Brand.ID);

            ChickWifiOnStart();
            ChickWifiContinuously();
        }

        private Product previousSelected;
        Product selectedProduct;
        const string TYPE_OF_STORE = "Brand";
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (value != null)
                {


                    _ = Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ProductPage(value,TYPE_OF_STORE));
                    previousSelected = value;

                    value = null;
                }
                selectedProduct = value;
                OnPropertyChanged();

            }
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
                    Products.Clear();
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                    Products.Clear();
                }
            };
        }

       
    }
}
