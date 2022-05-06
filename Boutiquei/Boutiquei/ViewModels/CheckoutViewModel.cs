using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.BoutiqueUserExceptions;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        //public Order Order { get; set; }

        public bool IsValid { set; get; }
        public bool IsNull { set; get; }

        private ObservableCollection<CartProduct> cartFromAPI { get; set; }
        private CartProduct cartProduct { set; get; }

        private Address address;
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        private string total;
        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }
        private string quantity;
        public string Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }
        private string totalAfterDelvery;
        public string TotalAfterDelvery
        {
            get
            {
                return totalAfterDelvery;
            }
            set
            {
                totalAfterDelvery = value;
                OnPropertyChanged();
            }
        }
        private readonly Random _random = new Random();
        private string orderNumber { set; get; }
        private string status { set; get; }

        private string date { set; get; }
        public string Delevry { set; get; }
        public AppServices Services;

        public ICommand OrderCommand { get; }
        public ICommand EditCommand { get; }

        

        public CheckoutViewModel()
        {

            Services = new AppServices();
            IsValid = false;
            IsNull = true;
            //Task.Run(async () => { await LoadData(); }).Wait();
            // _ = GetAddress();
            Address = new Address();

            Task.Run(async () => { await LoadData(); }).Wait();
            //Address = new Address { AddressDetails = "Faisal Street", City = "Nablus", Name = "Omar", Phone = "065316372", District = "Downtown" };
            //GetData();

            //Total = Services.GetTotalProductsPrice("User1").GetAwaiter().GetResult();
            cartFromAPI = new ObservableCollection<CartProduct>();
         
            cartFromAPI = Services.GetCartProductsByUserID();
            date = DateTime.Now.ToString("dd-MMM-yyyy");
            orderNumber = _random.Next(1, 100000).ToString();
            Delevry = "10";
            TotalAfterDelvery = (Convert.ToInt32(Total) + Convert.ToInt32(Delevry)).ToString();

            status = "Processing";


            OrderCommand = new Xamarin.Forms.Command(onOrderTapped);
            EditCommand = new Xamarin.Forms.Command(OnEditTapped);

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
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                }
            };
        }
        private async void OnEditTapped(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new OrderAddressPage());
            await LoadData();
            Console.WriteLine("hihiihihiihiihiihihih");
        }

        public async Task LoadData()
        {
            //Address = new Address();


            try
            {
                Total = await Services.GetTotalProductsPrice();
                Quantity = await Services.TotalProductsQuantity();
                Address = await Services.GetTheDefultAddress();
                OnPropertyChanged();
                if (Address == null)
                {
                    IsValid = false;
                    IsNull = true;
                    OnPropertyChanged();
                }
                else
                {
                    IsValid = true;
                    IsNull = false;
                    OnPropertyChanged();
                }
               
            }
            catch (NotFoundException e)
            {
                Address=null;
                Console.WriteLine("NotFoundException message : " + e.Message);
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception message : " + e.Message);
            }

           
        }

        

        private async void onOrderTapped(object obj)
        {
            
                Order order = new Order
                {
                    OrderDate = date,
                    OrderNumber = orderNumber,
                    OrderTotal = TotalAfterDelvery,
                    OrderStatus = status,
                    Quantity = Quantity
                };
            try
            {
               
                if (Address==null)
                {
                    
                    _ = Application.Current.MainPage.DisplayAlert("Pay attention", "Please add you address", "Ok");
                }
                else
                {
                    await Services.AddtoOrder(order);

                    for (int i=0;i<cartFromAPI.Count;i++)
                    {
                        await Services.AddCartToOrder(cartFromAPI[i], order.OrderNumber);
                    }
                    await Services.DeleteAllProductsInCart();
                    await Services.UpdateUserTotal();
                    Application.Current.MainPage = new NavigationPage(new SuccessPage());
                }
               
            }
            catch
            {
                await LoadData();
            }
        }
    }
}
