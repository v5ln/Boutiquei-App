using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Boutiquei.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Boutiquei.Services
{
    public class AppServices
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://boutiquei-54faf-default-rtdb.firebaseio.com/");

        //
        public ObservableCollection<Store> GetAllBoutiques()
        {
            return firebaseClient.Child("Stores").Child("Boutiques").AsObservable<Store>().AsObservableCollection();
        }
        //
        public ObservableCollection<Store> GetAllPrands()
        {
            return firebaseClient.Child("Stores").Child("Prands").AsObservable<Store>().AsObservableCollection();
        }


        //
        public ObservableCollection<Product> GetAllBoutiqueProducts(string BId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/").Child("Products").AsObservable<Product>().AsObservableCollection();
        }
        //
        public ObservableCollection<Product> GetAllBrandProducts(string BId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/").Child("Products").AsObservable<Product>().AsObservableCollection();
        }

       //
        public ObservableCollection<PImgs> GetAllBoutiqueProductImgs(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();
        }
        //
        public ObservableCollection<Colors> GetAllBoutiqueProductColors(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();
        }
        //
        public ObservableCollection<Sizes> GetAllBoutiqueProductSizes(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();
        }
        //
        public ObservableCollection<PImgs> GetAllBrandProductImgs(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();
        }
        //
        public ObservableCollection<Colors> GetAllBrandProductColors(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();
        }
        //
        public ObservableCollection<Sizes> GetAllBrandProductSizes(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();
        }

        //
        public ObservableCollection<Product> GetFavouriteProductsByUserID(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/Favourite").Child("Products").AsObservable<Product>().AsObservableCollection();
        }
        //
        public ObservableCollection<CartProduct> GetCartProductsByUserID(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").AsObservable<CartProduct>().AsObservableCollection();
        }
        //

        //public bool IsInFavourite(string UserID, string PID)
        //{
        //    ObservableCollection<Product> _products = GetFavouriteProductsByUserID(UserID);

        //    _products.CollectionChanged += isInFavouriteListChanged;

        //    foreach (var _product in _products)
        //    {
        //        Application.Current.MainPage.DisplayAlert("x", PID + "\n" + _product.PID, "x");
        //        if (_product.PID == PID)
        //        {
        //            Application.Current.MainPage.DisplayAlert("x", "OI OI ", "x");
        //            return true;
        //        }
        //    }
        //    Application.Current.MainPage.DisplayAlert("x", "PEPE ", "x");
        //    return false;
        //}

        //private void isInFavouriteListChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    ObservableCollection<Product> products = sender as ObservableCollection<Product>;
        //    if (e.Action == NotifyCollectionChangedAction.Add)
        //    {

        //    }
        //}

        //public bool IsInCart(string UserID, string PID)
        //{
        //    ObservableCollection<CartProduct> _products = GetCartProductsByUserID(UserID);
        //    foreach (var _product in _products)
        //    {
        //        if (_product.PID.Equals(PID)) return true;
        //    }
        //    return false;
        //}


        public async Task AddToFavourites(Product product, string UserID)
        {
            var product_ = (await firebaseClient
          .Child($"Users/{UserID}/Favourite").Child("Products")
         .OnceAsync<CartProduct>()).Where(a => a.Object.PID == product.PID).FirstOrDefault();

            if (product_ == null)
            {
                await firebaseClient.Child($"Users/{UserID}/Favourite").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's already added in the Favourite", "Ok");
            }
        }

        public async Task AddToCart(CartProduct product, string UserID)
        {
            await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            string total = firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) + (Convert.ToInt32(product.Price) * Convert.ToInt32(product.Quantity))).ToString();
            await firebaseClient
                    .Child($"Users/{UserID}/Cart").Child("Total")
                     .PutAsync(newTotal);
        }

        public async Task DeleteFromCart(string UserID, string PID)
        {
            var toUpdate = (await firebaseClient
             .Child($"Users/{UserID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").Child(toUpdate.Key).DeleteAsync();

            string total = firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) - ( Convert.ToInt32(toUpdate.Object.Price) * Convert.ToInt32(toUpdate.Object.Quantity)) ).ToString();
            await firebaseClient
                    .Child($"Users/{UserID}/Cart").Child("Total")
                     .PutAsync(newTotal);

        }

        public async Task DeleteFromFavourites(string UserID, string PID)
        {
            var toDeletePerson = (await firebaseClient
             .Child($"Users/{UserID}/Favourite").Child("Products")
             .OnceAsync<Product>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            await firebaseClient.Child($"Users/{UserID}/Favourite").Child("Products").Child(toDeletePerson.Key).DeleteAsync();
        }



        public async Task UpdateIncreaseQuantity(string UserID, string PID)
        {

            var toUpdate = (await firebaseClient
          .Child($"Users/{UserID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.Quantity = (Convert.ToInt32(toUpdate.Object.Quantity) + 1).ToString();

            //update the new value
            await firebaseClient
                    .Child($"Users/{UserID}/Cart").Child("Products")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);

            string total = firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) + Convert.ToInt32(toUpdate.Object.Price)).ToString();
            await firebaseClient
                    .Child($"Users/{UserID}/Cart").Child("Total")
                     .PutAsync(newTotal);
        }

        public async Task UpdateDecreaseQuantity(string UserID, string PID)
        {

            var toUpdate = (await firebaseClient
          .Child($"Users/{UserID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)

            if (!toUpdate.Object.Quantity.Equals("1"))
            {
                toUpdate.Object.Quantity = (Convert.ToInt32(toUpdate.Object.Quantity) - 1).ToString();


                //update the new value
                await firebaseClient
                      .Child($"Users/{UserID}/Cart").Child("Products")
                      .Child(toUpdate.Key)
                      .PutAsync(toUpdate.Object);
                string total = firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
                string newTotal = (Convert.ToInt32(total) - Convert.ToInt32(toUpdate.Object.Price)).ToString();
                await firebaseClient
                        .Child($"Users/{UserID}/Cart").Child("Total")
                         .PutAsync(newTotal);
            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's quantity must be higher than 1", "Ok");
            }
            
        }

        //public async Task<string> GetTotalProductsPrice(string UserID)
        //{
        //    var _products = await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").OnceAsync<CartProduct>();


        //    string total = _products.Sum(x => decimal.Parse(x.Object.Price, NumberStyles.Currency) * decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency)).ToString();
        //    //d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
        //    return total;
        //}

        //public async Task<string> GetTotalProductsPrice(string UserID)
        //{
        //    string total = "2";
        //    await Task.Run(() =>
        //    {
        //        var _products = GetCartProductsByUserID(UserID);

        //        //string total = _products.Sum(product => Convert.ToInt32(product.Price) * Convert.ToInt32(product.Quantity)).ToString();

        //        foreach (var product in _products)
        //        {
        //            total = (Convert.ToInt32(total) + (Convert.ToInt32(product.Price) * Convert.ToInt32(product.Quantity))).ToString();
        //        }
        //        Console.WriteLine(total);
        //        //d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
        //        return total;
        //    });
        //    return total;

        //}
        public async Task<string> GetTotalProductsPrice(string UserID)
        {
            return await firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>();
        }


        //Sprint 4 :

        public ObservableCollection<Address> GetAllAdressesByUserID(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/").Child("Addresses").AsObservable<Address>().AsObservableCollection();

        }


        public async Task AddNewAddress(Address address, string UserID)
        {
            await firebaseClient.Child($"Users/{UserID}/").Child("Addresses").PostAsync(JsonConvert.SerializeObject(address));
        }

        public async Task DeleteAddress(string UserID, string AddressID)
        {

            var toDeletePerson = (await firebaseClient
             .Child($"Users/{UserID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            await firebaseClient.Child($"Users/{UserID}/").Child("Addresses").Child(toDeletePerson.Key).DeleteAsync();

        }

        public async Task UpdateNotDefultAddress(string UserID, string AddressID)
        {


            var toUpdate = (await firebaseClient
             .Child($"Users/{UserID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.IsDefault = "0";

            //update the new value
            await firebaseClient
                    .Child($"Users/{UserID}").Child("Addresses")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);
        }

        public async Task UpdateDefultAddress(string UserID, string AddressID)
        {

            (await firebaseClient
             .Child($"Users/{UserID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID != AddressID).ToList().ForEach(async x =>
             {
                 await UpdateNotDefultAddress(UserID, x.Object.AddressID);
             }
             );


            var toUpdate = (await firebaseClient
             .Child($"Users/{UserID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.IsDefault = "1";

            //update the new value
            await firebaseClient
                    .Child($"Users/{UserID}").Child("Addresses")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);
        }

        public async Task<String> TotalProductsQuantity(string UserID)
        {
            var d = await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").OnceAsync<CartProduct>();


            var Quantity = d.Sum(x => decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency));
            // d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
            return Quantity.ToString();
        }
        public async Task AddtoOrder(string UserID, Order order)
        {
            // must be in MV :
            /*
             
             private readonly Random _random = new Random();
             Order order = new Order();

             order.OrderDate = DateTime.Now.ToString("dd-MMM-yyyy");
             order.OrderStatus = "Processing";
             order.OrderTotal = await TotalProductsPrice(UserID);
             order.Quantity = await TotalProductsQuantity(UserID);
             order.OrderNumber = _random.Next(1, 100000).ToString();
            */
            await firebaseClient.Child($"Users/{UserID}/").Child("Orders").PostAsync(JsonConvert.SerializeObject(order));



        }

        public async Task DeleteAllProductsInCart(string UserID)
        {
            await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").DeleteAsync();
        }

        public ObservableCollection<Order> GetOrders(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/").Child("Orders").AsObservable<Order>().AsObservableCollection();

        }


        public async Task<Address> GetTheDefultAddress(string UserID)
        {

            var AllAddresses = await firebaseClient.Child($"Users/{UserID}/").Child("Addresses").OnceAsync<Address>();
            Address defultAddress = AllAddresses.Where(x => x.Object.IsDefault == "1").Select(itm => itm.Object).FirstOrDefault();
            Address defultAddress_ = defultAddress as Address;
            return defultAddress_;
        }
    }
}
