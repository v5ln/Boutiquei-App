using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Boutiquei.BoutiqueUserExceptions;
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
                //throw new Exception("Firebase URL not valid");
                Application.Current.MainPage = new SplashScreenPage();
            }
            
        }
    
       

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


        private void checkResponse<T>(T value)
        {
            if (value == null)
            {
                Application.Current.MainPage.DisplayAlert("Faild", "Response status code 404('Not Founded')", "Ok");

            }
        }
        public ObservableCollection<Store> GetAllBoutiques()
        {
            var boutiquies =  firebaseClient.Child("Stores").Child("Boutiques").AsObservable<Store>().AsObservableCollection();
            //checkResponse(boutiquies);
            return boutiquies;

        }

        public ObservableCollection<Store> GetAllPrands()
        {
            var prands =  firebaseClient.Child("Stores").Child("Prands").AsObservable<Store>().AsObservableCollection();
            //checkResponse(prands);
            return prands;
        }


        public ObservableCollection<Product> GetAllBoutiqueProducts(string BId_)
        {
            var boutiqueProducts =  firebaseClient.Child($"Stores/Boutiques/{BId_}/").Child("Products").AsObservable<Product>().AsObservableCollection();
            //checkResponse(boutiqueProducts);
            return boutiqueProducts;
        }
        
        public ObservableCollection<Product> GetAllBrandProducts(string BId_)
        {
            var prandProducts = firebaseClient.Child($"Stores/Prands/{BId_}/").Child("Products").AsObservable<Product>().AsObservableCollection();
            //checkResponse(prandProducts);
            return prandProducts;
        }

        public ObservableCollection<PImgs> GetAllBoutiqueProductImgs(string BId_, string PId_)
        {
            var boutiqueProductImgs = firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();
            //checkResponse(boutiqueProductImgs);
            return boutiqueProductImgs;
        }
        
        public ObservableCollection<Colors> GetAllBoutiqueProductColors(string BId_, string PId_)
        {
            var boutiqueProductColors = firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();
            //checkResponse(boutiqueProductColors);
            return boutiqueProductColors;
        }
        
        public ObservableCollection<Sizes> GetAllBoutiqueProductSizes(string BId_, string PId_)
        {
            var boutiqueProductSizes = firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();
            //checkResponse(boutiqueProductSizes);
            return boutiqueProductSizes;
        }
        
        public ObservableCollection<PImgs> GetAllBrandProductImgs(string BId_, string PId_)
        {
            var brandProductImgs = firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();
            //checkResponse(brandProductImgs);
            return brandProductImgs;
        }
        
        public ObservableCollection<Colors> GetAllBrandProductColors(string BId_, string PId_)
        {
            var brandProductColors = firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();
            //checkResponse(brandProductColors);
            return brandProductColors;

        }
        
        public ObservableCollection<Sizes> GetAllBrandProductSizes(string BId_, string PId_)
        {
            var brandProductSizes = firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();
            //checkResponse(boutiquies);
            return brandProductSizes;

        }

        public ObservableCollection<Product> GetFavouriteProductsByUserID()
        {
            var favourites = firebaseClient.Child($"Users/{userID}/Favourite").Child("Products").AsObservable<Product>().AsObservableCollection();
            //checkResponse(favourites);
            return favourites;
        }
  
        public ObservableCollection<CartProduct> GetCartProductsByUserID()
        {
            var cart = firebaseClient.Child($"Users/{userID}/Cart").Child("Products").AsObservable<CartProduct>().AsObservableCollection();
            //checkResponse(cart);
            return cart;

        }
            
        public async Task AddToFavourites(Product product)
        {
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
            await firebaseClient.Child("Users").Child(userID).Child("Cart").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            string newTotal = (Convert.ToInt32(total) + (Convert.ToInt32(product.Price) * Convert.ToInt32(product.Quantity))).ToString();
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Total")
                     .PutAsync(newTotal);
        }

        public async Task DeleteFromCart(string PID)
        {
            var toUpdate = (await firebaseClient
                           .Child($"Users/{userID}/Cart").Child("Products")
                           .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            //checkResponse(toUpdate);
            await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").Child(toUpdate.Key).DeleteAsync();

            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            //checkResponse(total);
            string newTotal = (Convert.ToInt32(total) - ( Convert.ToInt32(toUpdate.Object.Price) * Convert.ToInt32(toUpdate.Object.Quantity)) ).ToString();

            if(newTotal != null)
            {
                 await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Total")
                     .PutAsync(newTotal);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "New Total = null", "Ok");
            }


        }

        public async Task DeleteFromFavourites(string PID)
        {
            var toDeletePerson = (await firebaseClient
                                 .Child($"Users/{userID}/Favourite").Child("Products")
                                 .OnceAsync<Product>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            //checkResponse(toDeletePerson);

            await firebaseClient.Child($"Users/{userID}/Favourite").Child("Products").Child(toDeletePerson.Key).DeleteAsync();
        }



        public async Task UpdateIncreaseQuantity(string PID)
        {
            var toUpdate = (await firebaseClient
                           .Child($"Users/{userID}/Cart").Child("Products")
                           .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            //checkResponse(toUpdate);

            // modify your data (toUpdate is your old object value)
            toUpdate.Object.Quantity = (Convert.ToInt32(toUpdate.Object.Quantity) + 1).ToString();

            //update the new value
            await firebaseClient
                    .Child($"Users/{userID}/Cart").Child("Products")
                    .Child(toUpdate.Key)
                     .PutAsync(toUpdate.Object);

            //dublicate code , we will have update total method "use it here":
            string total = firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
            //checkResponse(total);

            string newTotal = (Convert.ToInt32(total) + Convert.ToInt32(toUpdate.Object.Price)).ToString();

            if(newTotal != null)
            {
                await firebaseClient
                      .Child($"Users/{userID}/Cart").Child("Total")
                      .PutAsync(newTotal);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "New Total = null", "Ok");
            }

        }

        public async Task UpdateDecreaseQuantity( string PID)
        {

            //this api is exist :: for editing => call GetCartProductsByUserID() function :
            var toUpdate = (await firebaseClient
                            .Child($"Users/{userID}/Cart").Child("Products")
                            .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();
            //checkResponse(toUpdate);

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
                //checkResponse(total);

                string newTotal = (Convert.ToInt32(total) - Convert.ToInt32(toUpdate.Object.Price)).ToString();

                if (newTotal != null)
                {
                    await firebaseClient
                          .Child($"Users/{userID}/Cart").Child("Total")
                          .PutAsync(newTotal);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Faild", "New Total = null", "Ok");
                }

            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's quantity must be higher than 1", "Ok");
            }
            
        }

        public async Task<string> GetTotalProductsPrice()
        {
            var productsPrice = await firebaseClient.Child($"Users/{userID}/Cart").Child("Total").OnceSingleAsync<string>();
            //checkResponse(productsPrice);
            return productsPrice;
        }


        public ObservableCollection<Address> GetAllAdressesByUserID()
        {
            var allAdresses = firebaseClient.Child($"Users/{userID}/").Child("Addresses").AsObservable<Address>().AsObservableCollection();
            //checkResponse(allAdresses);
            return allAdresses;

        }


        public async Task AddNewAddress(Address address)
        {
            await firebaseClient.Child($"Users/{userID}/").Child("Addresses").PostAsync(JsonConvert.SerializeObject(address));
        }

        public async Task DeleteAddress(string AddressID)
        {

            var toDeletePerson = (await firebaseClient
                                 .Child($"Users/{userID}/").Child("Addresses")
                                 .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            //checkResponse(toDeletePerson);

            await firebaseClient.Child($"Users/{userID}/").Child("Addresses").Child(toDeletePerson.Key).DeleteAsync();

        }

        public async Task UpdateNotDefultAddress(string AddressID)
        {
            var toUpdate = (await firebaseClient
                           .Child($"Users/{userID}/").Child("Addresses")
                           .OnceAsync<Address>()).Where(a => a.Object.AddressID == AddressID).FirstOrDefault();
            //checkResponse(toUpdate);

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

            //checkResponse(toUpdate);

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
            //checkResponse(products);

            var Quantity = products.Sum(x => decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency));

            //checkResponse(Quantity);

            return Quantity.ToString();


        }

        public async Task AddtoOrder( Order order)
        {

            await firebaseClient.Child($"Users/{userID}/").Child("Orders").PostAsync(JsonConvert.SerializeObject(order));

        }

        public async Task AddCartToOrder(CartProduct product,string orderNumber)
        {

           var order =  firebaseClient.Child($"Users/{userID}/").Child("Orders").OnceAsync<Order>().GetAwaiter().GetResult().Where(orderItem => orderItem.Object.OrderNumber == orderNumber).FirstOrDefault();
            //checkResponse(order);

            await firebaseClient.Child($"Users/{userID}/").Child($"Orders/{order.Key}").Child("Products").PostAsync(JsonConvert.SerializeObject(product));

        }

        public ObservableCollection<CartProduct> GetOrderProductsByUserID(string orderNumber)
        {
            var order = firebaseClient.Child($"Users/{userID}/").Child("Orders").OnceAsync<Order>().GetAwaiter().GetResult().Where(orderItem => orderItem.Object.OrderNumber == orderNumber).FirstOrDefault();
            //checkResponse(order);
            
            var orderProducts = firebaseClient.Child($"Users/{userID}/").Child($"Orders/{order.Key}").Child("Products").AsObservable<CartProduct>().AsObservableCollection();
            //checkResponse(orderProducts);
            return orderProducts;
        }
        public async Task DeleteAllProductsInCart()
        {
            await firebaseClient.Child($"Users/{userID}/Cart").Child("Products").DeleteAsync();
        }

        public ObservableCollection<Order> GetOrders()
        {
            var orders = firebaseClient.Child($"Users/{userID}/").Child("Orders").AsObservable<Order>().AsObservableCollection();
            //checkResponse(orders);
            return orders;

        }


        public async Task<Address> GetTheDefultAddress()
        {

            var allAddresses = await firebaseClient.Child($"Users/{userID}/").Child("Addresses").OnceAsync<Address>();
            //checkResponse(allAddresses);

            Address defultAddress = allAddresses.Where(x => x.Object.IsDefault == "1").Select(itm => itm.Object).FirstOrDefault();
            //checkResponse(defultAddress);

            /* if (defultAddress == null)
             {
               //  throw new NotFoundException("StatusCode from GetTheDefultAddress API : " + ((int)HttpStatusCode.NOT_FOUND));
             }*/
            return defultAddress;
        }

        public async Task AddNewUser(AppUser user)
        {
            _ = LoadToken();
            user.Token = Token;
            await firebaseClient.Child("Users").PostAsync(JsonConvert.SerializeObject(user));

        }


        public async Task<AppUser> GetUserDetails()
        {
            var userDetails = await firebaseClient.Child($"Users/{userID}").OnceSingleAsync<AppUser>();
            // checkResponse(boutiquies);
            return userDetails;
        }

        public async Task UpdateUserTotal()
        {
           const string RESET_TOTAL = "0";
           await firebaseClient.Child($"Users/{userID}").Child("Cart").Child("Total").PutAsync(RESET_TOTAL);
        }
    }

    public enum HttpStatusCode
    {
        OK = 200,
        BAD_REQUEST = 400,
        NOT_FOUND = 404,
        INTERNAL_SERVER = 500,
    }
}
 