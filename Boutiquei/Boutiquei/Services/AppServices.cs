using System;
using System.Collections.ObjectModel;
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
        //public string GetCartTotalByUserID(string UserID)
        //{
        //    return firebaseClient.Child($"Users/{UserID}/Cart").Child("Total").OnceSingleAsync<string>().GetAwaiter().GetResult();
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

        public async Task AddToCart(Product product, string UserID)
        {
            var product_ = (await firebaseClient
            .Child($"Users/{UserID}/Cart").Child("Products")
            .OnceAsync<CartProduct>()).Where(a => a.Object.PID == product.PID).FirstOrDefault();

            if (product_ == null)
            {
               await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").PostAsync(JsonConvert.SerializeObject(product));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's already added to cart", "Ok");
            }
        }


        public async Task DeleteFromCart(string UserID, string PID)
        {
            var toDeletePerson = (await firebaseClient
             .Child($"Users/{UserID}/Cart").Child("Products")
             .OnceAsync<CartProduct>()).Where(a => a.Object.PID == PID).FirstOrDefault();

            await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").Child(toDeletePerson.Key).DeleteAsync();

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
            }

            else
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "The product's quantity must be higher than 1", "Ok");
            }
            
        }

        public async Task<string> GetTotalProductsPrice(string UserID)
        {
            var _products = await firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").OnceAsync<CartProduct>();


            string total = _products.Sum(x => decimal.Parse(x.Object.Price, NumberStyles.Currency) * decimal.Parse(Convert.ToString(x.Object.Quantity), NumberStyles.Currency)).ToString();
            //d.Select(x => x.Object.Total = Convert.ToInt32(total * Convert.ToInt32(x.Object.Quantity)));
            return total;
        }

    }
}
