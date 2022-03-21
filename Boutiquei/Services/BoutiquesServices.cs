using Boutiquei.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutiquei.Services
{
    class BoutiquesServices
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://boutique-mob-app-default-rtdb.firebaseio.com/");


        public async Task<List<StoreModel>> GetAllBoutiques()
        {

            return (await firebaseClient
           .Child("Stores").OnceAsync<StoreModel>()).Select(item => new StoreModel
           {

               Id = item.Key,
               BName = item.Object.BName,
               BMainPic = item.Object.BMainPic,
               Type = item.Object.Type,
               BCoverPic = item.Object.BCoverPic,
              

           }).Where(s => s.Type == "Boutique").ToList();



        }


        public async Task<List<ProductModel>> GetAllProducts(string BId_)
        {
            return (await firebaseClient.Child($"Stores/{BId_}/").Child("ProductModel").OnceAsync<ProductModel>()).Select(item => new ProductModel
            {
                

                Id = item.Key,
                PName = item.Object.PName,
                PPrice = item.Object.PPrice,
                ProductImage = item.Object.ProductImage,
                StoreID = BId_

            }).ToList();

        }

    }
}
