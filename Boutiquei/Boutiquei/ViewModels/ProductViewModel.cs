using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Boutiquei.Models;
using System.Collections.Generic;
using Boutiquei.Services;
using Boutiquei.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
namespace Boutiquei.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ObservableCollection<PImgs> ProductImages { get; set; }
        public ObservableCollection<Sizes> ProductSizes { get; set; }
        public ObservableCollection<Colors> ProductColores { get; set; }
        public CartProduct cartProduct { set; get; }

        public string Quantity { set; get; }

        AppServices services = new AppServices();
        

        public ProductViewModel(Product product)
        {
            Product = new Product();
            Product = product;
            ProductImages = new ObservableCollection<PImgs>();
            ProductSizes = new ObservableCollection<Sizes>();
            ProductColores = new ObservableCollection<Colors>();

            Quantity = "1";
            ProductImages = services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = services.GetAllBoutiqueProductColors(Product.BID, Product.PID);
        }


        // when add to cart btn 
        //public void AddToCart()
        //{
        //    cartProduct = new CartProduct({Quantity = Quantity  })
        //    services.AddToCart(cartProduct)
        //}
        

    }
}
