using System;
using System.Collections.ObjectModel;

namespace Boutiquei.ViewModels
{
    public class ProductViewModel
    {
        public Products Product { get; set; }
        public ObservableCollection<PImgs> ProductImages { get; set; }
        public ObservableCollection<Sizes> ProductSizes { get; set; }
        public ObservableCollection<Colors> ProductColores { get; set; }
        AppServicess services = new AppServicess();


        public ProductViewModel(Products product)
        {
            Product = new Products();
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
