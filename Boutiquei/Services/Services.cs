using Boutiquei.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Boutiquei.Services
{
    class Services
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
    }
}
