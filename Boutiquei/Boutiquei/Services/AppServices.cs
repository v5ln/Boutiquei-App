using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Boutiquei.Models;
using Boutiquei.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Boutiquei.Services
{
    public class AppServices
    {

        private static string firebaseURL = "https://boutiquei-54faf-default-rtdb.firebaseio.com/";
        FirebaseClient firebaseClient = new FirebaseClient(firebaseURL);

        public static void CheckURLValid(string firebaseURL, out Uri resultURI)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(firebaseURL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            resultURI = null;
            if (result == false)
            {
                throw new Exception("Firebase URL not valid");
            }
            
        }
    
        //

        private static string userID { get; set; }
        public static string Token { get; set; }
        public AppServices()
        {
            try
            {
                Uri uriResult;
                CheckURLValid(firebaseURL,out uriResult);
                _ = LoadToken();
            }
            catch (Exception e)
            {
                Console.WriteLine("URL validate Exception : " + e.Message);
                Application.Current.MainPage.DisplayAlert("Pay attention", "Firebase URL not valid", "Ok");
            }
            
        }

        private async Task LoadToken()
        {
            Token = await SecureStorage.GetAsync("oauth_token");
            userID = (await firebaseClient
            .Child("Users")
            .OnceAsync<AppUser>()).Where(a => a.Object.Token == Token).FirstOrDefault().Key;
        }

        public ObservableCollection<Store> GetAllBoutiques()
        {

             
            var boutiquies =  firebaseClient.Child("Stores").Child("Boutiques").AsObservable<Store>().AsObservableCollection();
      /*      if(Boutiquies == null)
            {
                throw new Exception("Status code 500");
            }
*/
            return boutiquies;


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

        //////////
        public ObservableCollection<Product> GetFavouriteProductsByUserID()
        {
            return firebaseClient.Child($"Users/{userID}/Favourite").Child("Products").AsObservable<Product>().AsObservableCollection();
        }
        //
        public ObservableCollection<CartProduct> GetCartProductsByUserID()
        {
            return firebaseClient.Child($"Users/{userID}/Cart").Child("Products").AsObservable<CartProduct>().AsObservableCollection();
        }
        //

        //public bool IsInFavourite(, string PID)
        //{
        //    ObservableCollection<Product> _products = GetFavouriteProductsByUserID(userID);

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

        //public bool IsInCart(, string PID)
        //{
        //    ObservableCollection<CartProduct> _products = GetCartProductsByUserID(userID);
        //    foreach (var _product in _products)
        //    {
        //        if (_product.PID.Equals(PID)) return true;
        //    }
        //    return false;
        //}


        public async Task AddToFavourites(Product product)
        {

            //this api is exist :: for editing => call GetFavouriteProductsByUserID() function :
            var product_ = (await firebaseClient
          .Child($"Users/{userID}/Favourite").Child("Products")
         .OnceAsync<CartProduct>()).Where(a => a.Object.PID == product.PID).FirstOrDefault();

            if (product_ == null)
            {
                await firebaseClient.Child($"Users/{userID}/Favourite").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's already added in the Favourite", "Ok");
            }
        }

        public async Task AddToCart(CartProduct product )
        {
           //split update total api from post product to cart :
            await firebaseClient.Child("Users").Child(userID).Child("Cart").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) + (Convert.ToInt32(product.Price) * Convert.ToInt32(product.Quantity))).ToString();
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Total")
                     .PutAsync(newTotal);


        }

        public async Task DeleteFromCart(string PID)
        {

            //this api is exist :: for editing => call GetCartProductsByUserID() function :
            var toUpdate = (await firebaseClient
             .Child($"Users/{userID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").Child(toUpdate.Key).DeleteAsync();


            //dublicate code , we will have update total method "use it here":
            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) - ( Convert.ToInt32(toUpdate.Object.Price) * Convert.ToInt32(toUpdate.Object.Quantity)) ).ToString();
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Total")
                     .PutAsync(newTotal);

        }

        public async Task DeleteFromFavourites(string PID)
        {

            //this api is exist :: for editing => call GetFavouriteProductsByUserID() function :
            var toDeletePerson = (await firebaseClient
             .Child($"Users/{userID}/Favourite").Child("Products")
             .OnceAsync<Product>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            await firebaseClient.Child($"Users/{userID}/Favourite").Child("Products").Child(toDeletePerson.Key).DeleteAsync();
        }



        public async Task UpdateIncreaseQuantity(string PID)
        {

            //this api is exist :: for editing => call GetCartProductsByUserID() function :
            var toUpdate = (await firebaseClient
          .Child($"Users/{userID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.Quantity = (Convert.ToInt32(toUpdate.Object.Quantity) + 1).ToString();

            //update the new value
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Products")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);

            //dublicate code , we will have update total method "use it here":
            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) + Convert.ToInt32(toUpdate.Object.Price)).ToString();
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Total")
                     .PutAsync(newTotal);
        }

        public async Task UpdateDecreaseQuantity( string PID)
        {

            //this api is exist :: for editing => call GetCartProductsByUserID() function :
            var toUpdate = (await firebaseClient
            .Child($"Users/{userID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)

            if (!toUpdate.Object.Quantity.Equals("1"))
            {
                toUpdate.Object.Quantity = (Convert.ToInt32(toUpdate.Object.Quantity) - 1).ToString();


                //update the new value
                await firebaseClient
                      .Child($"Users/{userID}/Cart").Child("Products")
                      .Child(toUpdate.Key)
                      .PutAsync(toUpdate.Object);

                //dublicate code , we will have update total method "use it here":
                string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
                string newTotal = (Convert.ToInt32(total) - Convert.ToInt32(toUpdate.Object.Price)).ToString();
                await firebaseClient
                        .Child($"Users/{userID}/Cart").Child("Total")
                         .PutAsync(newTotal);
            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's quantity must be higher than 1", "Ok");
            }
            
        }

        //public async Task<string> GetTotalProductsPrice()
        //{
        //    var _products = await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").OnceAsync<CartProduct>();


        //    string total = _products.Sum(x => decimal.Parse(x.Object.Price, NumberStyles.Currency) * decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency)).ToString();
        //    //d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
        //    return total;
        //}

        //public async Task<string> GetTotalProductsPrice()
        //{
        //    string total = "2";
        //    await Task.Run(() =>
        //    {
        //        var _products = GetCartProductsByUserID(userID);

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
        public async Task<string> GetTotalProductsPrice()
        {
            return await firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>();
        }


        //Sprint 4 :

        public ObservableCollection<Address> GetAllAdressesByUserID()
        {
            return firebaseClient.Child($"Users/{userID}/").Child("Addresses").AsObservable<Address>().AsObservableCollection();

        }


        public async Task AddNewAddress(Address address)
        {
            await firebaseClient.Child($"Users/{userID}/").Child("Addresses").PostAsync(JsonConvert.SerializeObject(address));
        }

        public async Task DeleteAddress(string AddressID)
        {

            //this api is exist :: for editing => call GetAllAdressesByUserID() function :

            var toDeletePerson = (await firebaseClient
             .Child($"Users/{userID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            await firebaseClient.Child($"Users/{userID}/").Child("Addresses").Child(toDeletePerson.Key).DeleteAsync();

        }

        public async Task UpdateNotDefultAddress(string AddressID)
        {


            var toUpdate = (await firebaseClient
             .Child($"Users/{userID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.IsDefault = "0";

            //update the new value
            await firebaseClient
                    .Child($"Users/{userID}").Child("Addresses")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);
        }

        public async Task UpdateDefultAddress(string AddressID)
        {

            (await firebaseClient
             .Child($"Users/{userID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID != AddressID).ToList().ForEach(async x =>
             {
                 await UpdateNotDefultAddress(x.Object.AddressID);
             }
             );


            var toUpdate = (await firebaseClient
             .Child($"Users/{userID}/").Child("Addresses")
             .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            // modify your data (toUpdate is your old object value)
            toUpdate.Object.IsDefault = "1";

            //update the new value
            await firebaseClient
                    .Child($"Users/{userID}").Child("Addresses")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);
        }

        public async Task<String> TotalProductsQuantity()
        {
            var products = await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").OnceAsync<CartProduct>();


            var Quantity = products.Sum(x => decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency));
            // d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
            return Quantity.ToString();
        }

        public async Task AddtoOrder( Order order)
        {
            // must be in MV :
            /*
             
             private readonly Random _random = new Random();
             Order order = new Order();

             order.OrderDate = DateTime.Now.ToString("dd-MMM-yyyy");
             order.OrderStatus = "Processing";
             order.OrderTotal = await TotalProductsPrice(userID);
             order.Quantity = await TotalProductsQuantity(userID);
             order.OrderNumber = _random.Next(1, 100000).ToString();
            */
            await firebaseClient.Child($"Users/{userID}/").Child("Orders").PostAsync(JsonConvert.SerializeObject(order));



        }

        public async Task DeleteAllProductsInCart()
        {
            await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").DeleteAsync();
        }

        public ObservableCollection<Order> GetOrders()
        {
            return firebaseClient.Child($"Users/{userID}/").Child("Orders").AsObservable<Order>().AsObservableCollection();

        }


        public async Task<Address> GetTheDefultAddress()
        {

            var AllAddresses = await firebaseClient.Child($"Users/{userID}/").Child("Addresses").OnceAsync<Address>();
            Address defultAddress = AllAddresses.Where(x => x.Object.IsDefault == "1").Select(itm => itm.Object).FirstOrDefault();
            return defultAddress;
        }

        public async Task AddNewUser(AppUser user)
        {
            _ = LoadToken();
            //Task.Run(async () => { await LoadToken(); }).Wait();
            user.Token = Token;
            await firebaseClient.Child("Users").PostAsync(JsonConvert.SerializeObject(user));

            //var key = (await firebaseClient
            // .Child("Users")
            // .OnceAsync<AppUser>()).Where(a => a.Object.Token == accessToken).FirstOrDefault();
            //await firebaseClient
            //        .Child($"Users/{key}").Child("Cart").Child("Total")
            //        .PostAsync(0);

        }


        public async Task<AppUser> GetUserDetails()
        {
            return await firebaseClient.Child($"Users/{userID}").OnceSingleAsync<AppUser>();    
        }

        public async Task UpdateUserTotal()
        {
           await firebaseClient.Child($"Users/{userID}").Child("Cart").Child("Total").PutAsync("0");
        }
    }
}
