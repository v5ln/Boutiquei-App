using System;
using System.Collections.ObjectModel;
using Boutiquei.Models;
using Boutiquei.Services;
using Boutiquei.ViewModels;

[assembly: Xamarin.Forms.Dependency(typeof(SingleBrandViewModel))]
namespace Boutiquei.ViewModels
{
    public class SingleBrandViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Store Brand { get; set; }
        public AppServices Services { get; set; }
        public SingleBrandViewModel(Store brand)
        {
            Products = new ObservableCollection<Product>();
            Brand = new Store();
            this.Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            Services = new AppServices();
            Products = Services.GetAllBrandProducts(Brand.ID);
        }
    }
}
