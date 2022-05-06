using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.ViewModels;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(SingleBoutiqueViewModel))]
namespace Boutiquei.ViewModels
{
    public class SingleBoutiqueViewModel : BaseViewModel
    {

        public ObservableCollection<Product> Products { get; set; }
        public Store Boutique { get; set; }
        private readonly AppServices services;



        public SingleBoutiqueViewModel(Store boutique)
        {
            
            Products = new ObservableCollection<Product>();
            Boutique = new Store();
            this.Boutique = boutique ?? throw new ArgumentNullException(nameof(boutique));
            services = new AppServices();
            Products = services.GetAllBoutiqueProducts(Boutique.ID);
            
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
        private Product previousSelected;
        Product selectedProduct;
        const string TYPE_OF_STORE = "Boutique";
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (value != null)
                {

                    
                    Application.Current.MainPage.Navigation.PushAsync(new ProductPage(value, TYPE_OF_STORE));
                    previousSelected = value;

                    value = null;
                }
                selectedProduct = value;
                OnPropertyChanged();

            }
        }
        //public StoreModel GetStore(string ID)
        //{
        //    return Boutiques.Single(x => x.Id == ID);
        //}

    }
}

