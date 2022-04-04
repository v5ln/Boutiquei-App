using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace Boutiquei.Services
{
    public class AppServices
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://boutiquei-54faf-default-rtdb.firebaseio.com/");


        public ObservableCollection<Store> GetAllBoutiques()
        {
            return firebaseClient.Child("Stores").Child("Boutiques").AsObservable<Store>().AsObservableCollection();
        }

        public ObservableCollection<Store> GetAllPrands()
        {
            return firebaseClient.Child("Stores").Child("Prands").AsObservable<Store>().AsObservableCollection();
        }

        public ObservableCollection<Product> GetProductsForBoutiques(string id)
        {
            return firebaseClient.Child("Stores").Child($"Boutiques/{id}/").AsObservable<Product>().AsObservableCollection();
        }

        //Sprint 3 :
        //رح ابعت الربردكت اللي فيه اي دي للبوتيك وال اي للبردكت وابعتهم للاي بس اي وارحع ثلاث كولكنشن
        public ObservableCollection<PImgs> GetAllBoutiqueProductImgs(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();

        }

        public ObservableCollection<Colors> GetAllBoutiqueProductColors(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();

        }

        public ObservableCollection<Sizes> GetAllBoutiqueProductSizes(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Boutiques/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();

        }

        public ObservableCollection<PImgs> GetAllBrandProductImgs(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("PImgs").AsObservable<PImgs>().AsObservableCollection();

        }

        public ObservableCollection<Colors> GetAllBrandProductColors(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Colors").AsObservable<Colors>().AsObservableCollection();

        }

        public ObservableCollection<Sizes> GetAllBrandProductSizes(string BId_, string PId_)
        {
            return firebaseClient.Child($"Stores/Prands/{BId_}/Products/{PId_}/").Child("Sizes").AsObservable<Sizes>().AsObservableCollection();

        }


        //المفضلة 

        public ObservableCollection<Product> GetFavouriteProductsByUserID(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/Favourite").Child("Products").AsObservable<Product>().AsObservableCollection();

        }

        public ObservableCollection<Product> GetCartProductsByUserID(string UserID)
        {
            return firebaseClient.Child($"Users/{UserID}/Cart").Child("Products").AsObservable<Product>().AsObservableCollection();

        }

        public async Task<bool> Save(Product product, string UserID)
        {
            var data = await firebaseClient.Child($"Users/User1/Cart").Child("Products").PostAsync(JsonConvert.SerializeObject(product));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }

            return false;
        }

    }
}
