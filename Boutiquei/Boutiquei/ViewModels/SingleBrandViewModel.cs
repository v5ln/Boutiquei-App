using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.ViewModels;
using MvvmHelpers;
using Plugin.Connectivity;

[assembly: Xamarin.Forms.Dependency(typeof(SingleBrandViewModel))]
namespace Boutiquei.ViewModels
{
    public class SingleBrandViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Store Brand { get; set; }
        public AppServices Services { get; set; }
        public SingleBrandViewModel(Store brand)
        {
            Products = new ObservableCollection<Product>();
            Brand = new Store();
            this.Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            Services = new AppServices();
            Products = Services.GetAllBrandProducts(Brand.ID);

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
    }
}
