using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// using Boutiquei.Services;
using Boutiquei.Views;

namespace Boutiquei
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        // public async Task<List<BoutiqueiModel>> GetAll()
        // {

        //   return (await firebaseClient
        //  .Child("Stores").OnceAsync<BoutiqueiModel>()).Select(item => 
        //       {

        //           Id = item.Key,
        //           BName = item.Object.BName,
        //           BMainPic = item.Object.BMainPic,
        //           Type = item.Object.Type,
        //           BCoverPic = item.Object.BCoverPic,
        //           //


        //       })
        //       .Select(Item.Object.Colors =>  )
        //       .Where(s => s.Type == "Boutique").ToList(); 



        // }
        // public async Task<List<String>> GetAll()
        // {

        //   return (await firebaseClient
        //  .Child("Stores/color").OnceAsync<<List<String>>()).Select(item => new List<String>
        //       {

        //           colors.append(item)


        //       })
        //       .Select(Item.Object.Colors =>  )
        //       .Where(s => s.Type == "Boutique").ToList(); // [gr]
        // }


        // p1

        // Color : 
        // list<string> colors;

        
        // Product{
        // public String Name = "p1";
        // public List<String> Color = ["green","blue"];

        // }


    }
}
