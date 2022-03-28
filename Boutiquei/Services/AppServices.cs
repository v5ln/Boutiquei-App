﻿using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Firebase.Database;
using Firebase.Database.Query;

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

        //public ObservableCollection<Product> GetAllProduct(string BId_)
        //{
        //    return (await firebaseClient.Child($"Boutiques/{BId_}/").Child("Products").AsObservable<Product>().AsObservableCollection();

        //}
    }
}
