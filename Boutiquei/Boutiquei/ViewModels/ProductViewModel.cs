using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;

namespace Boutiquei.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public ObservableCollection<PImgs> ProductImages { get; set; }
        public ObservableCollection<Sizes> ProductSizes { get; set; }
        public ObservableCollection<Colors> ProductColores { get; set; }
        AppServices services = new AppServices();


        public ProductViewModel(Product product)
        {
            Product = new Product();
            Product = product;
            ProductImages = new ObservableCollection<PImgs>();
            ProductSizes = new ObservableCollection<Sizes>();
            ProductColores = new ObservableCollection<Colors>();

            ProductImages = services.GetAllBoutiqueProductImgs(Product.BID, Product.PID);
            ProductSizes = services.GetAllBoutiqueProductSizes(Product.BID, Product.PID);
            ProductColores = services.GetAllBoutiqueProductColors(Product.BID, Product.PID);
        }
    }
}
