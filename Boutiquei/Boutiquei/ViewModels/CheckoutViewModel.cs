using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        //public Order Order { get; set; }


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

        public CheckoutViewModel()
        {

            Services = new AppServices();

            //Task.Run(async () => { await LoadData(); }).Wait();
            // _ = GetAddress();
            //Task.Run(async () => { await LoadData(); }).Wait();
            Address = new Address();
            Address = new Address { AddressDetails = "Faisal Street", City = "Nablus", Name = "Omar", Phone = "065316372", District = "Downtown" };
            //GetData();
            Total = "100";
            //Total = Services.GetTotalProductsPrice("User1").GetAwaiter().GetResult();

            date = DateTime.Now.ToString("dd-MMM-yyyy");
            orderNumber = _random.Next(1, 100000).ToString();
            Delevry = "10";
            TotalAfterDelvery = (Convert.ToInt32(Total) + Convert.ToInt32(Delevry)).ToString();

            status = "Processing";


            OrderCommand = new Xamarin.Forms.Command(onOrderTapped);



        }


        
        public async Task LoadData()
        {
            Address = new Address();


            try
            {
                Address = await Services.GetTheDefultAddress();
                Total = await Services.GetTotalProductsPrice();
                Quantity = await Services.TotalProductsQuantity();
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
            await Services.AddtoOrder(order);

            Application.Current.MainPage = new SuccessPage();

        }
    }
}
