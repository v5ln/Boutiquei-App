﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Boutiquei.Models;
using Boutiquei.ViewModels;
using MvvmHelpers;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(SingleBoutiqueViewModel))]
namespace Boutiquei.ViewModels
{
    public class SingleBoutiqueViewModel : BaseViewModel
    {
        public List<Product> Products { get; set; }
        public List<Store> Boutiques { get; set; }
        
        

        public SingleBoutiqueViewModel()
        {
            //Boutiques = new List<StoreModel>();
            //var image1 = "https://cdn.discordapp.com/attachments/924024471755567124/955258823843676170/275766656_391237242376590_1553927296572176900_n.png";
            //var image2 = "https://cdn.discordapp.com/attachments/924024471755567124/955258843556880394/275379707_540699471027454_4428530398476858160_n.png";
            //var image3 = "https://cdn.discordapp.com/attachments/924024471755567124/955258857976901712/275955822_4840494826067082_1042685898013086821_n.png";
            //var image4 = "https://cdn.discordapp.com/attachments/924024471755567124/955258872136871986/275073208_537716854362551_8018523921874305659_n.png";
            //var image5 = "https://cdn.discordapp.com/attachments/924024471755567124/955258966651330590/274720697_986498812285985_8840338544609036108_n.png";
            //var image6 = "https://cdn.discordapp.com/attachments/924024471755567124/955258978554773594/276034740_491152432388036_7182820544372323363_n.png";
            //var image7 = "https://cdn.discordapp.com/attachments/924024471755567124/955258994400825354/275843570_1085016752055316_3369223921635088145_n.png";
            //var cover = "https://cdn.discordapp.com/attachments/924024471755567124/955259005935173682/275419393_1569353190117359_5624166761770086930_n.png";
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image1, BName = "Lana Line", Id = "s1", Type = "Boutique" });
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image2, BName = "Nagham Zalabieh", Id = "s2", Type = "Boutique" });
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image3, BName = "Wessam Qutob", Id = "s3", Type = "Boutique" });
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image4, BName = "Hussam Silawy", Id = "s4", Type = "Boutique" });
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image1, BName = "Lana Line", Id = "s5", Type = "Boutique" });
            //Boutiques.Add(new StoreModel { BCoverPic = cover, BMainPic = image2, BName = "Nagham Zalabieh", Id = "s6", Type = "Boutique" });

            //Products = new List<ProductModel>();
            //Products.Add(new ProductModel { Id = "p1", PName = "Product one", PPrice = "45", StoreID = "s1", ProductImage = image5});
            //Products.Add(new ProductModel { Id = "p2", PName = "Product two", PPrice = "99", StoreID = "s1", ProductImage = image6 });
            //Products.Add(new ProductModel { Id = "p3", PName = "Product three", PPrice = "100", StoreID = "s1", ProductImage = image7 });
            //Products.Add(new ProductModel { Id = "p4", PName = "Product four", PPrice = "13", StoreID = "s1", ProductImage = image5 });
            //Products.Add(new ProductModel { Id = "p5", PName = "Product 5", PPrice = "24", StoreID = "s1", ProductImage = image6 });
            //Products.Add(new ProductModel { Id = "p6", PName = "Product 6", PPrice = "52", StoreID = "s1", ProductImage = image7 });
            //Products.Add(new ProductModel { Id = "p7", PName = "Product 7", PPrice = "84", StoreID = "s2", ProductImage = image5 });
            //Products.Add(new ProductModel { Id = "p8", PName = "Product 8", PPrice = "14", StoreID = "s2", ProductImage = image6 });
            //Products.Add(new ProductModel { Id = "p9", PName = "Product 9", PPrice = "56", StoreID = "s2", ProductImage = image7 });
            //Products.Add(new ProductModel { Id = "p10", PName = "Product 10", PPrice = "32", StoreID = "s2", ProductImage = image5 });
            //Products.Add(new ProductModel { Id = "p11", PName = "Product 11", PPrice = "46", StoreID = "s3", ProductImage = image6 });
            //Products.Add(new ProductModel { Id = "p12", PName = "Product 12", PPrice = "77", StoreID = "s3", ProductImage = image7 });
            //Products.Add(new ProductModel { Id = "p13", PName = "Product 13", PPrice = "66", StoreID = "s3", ProductImage = image5 });
            //Products.Add(new ProductModel { Id = "p14", PName = "Product 14", PPrice = "55", StoreID = "s3", ProductImage = image6 });
            //Products.Add(new ProductModel { Id = "p15", PName = "Product 15", PPrice = "44", StoreID = "s3", ProductImage = image7 });
            //Products.Add(new ProductModel { Id = "p16", PName = "Product 16", PPrice = "33", StoreID = "s3", ProductImage = image5 });



        }
        //public StoreModel GetStore(string ID)
        //{
        //    return Boutiques.Single(x => x.Id == ID);
        //}

    }
}

