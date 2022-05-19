using Boutiquei.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Boutiquei.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsPage : ContentPage
    {
        public OrderDetailsPage(string orderNumber)
        {
            InitializeComponent();
            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel(orderNumber);
            BindingContext = orderDetailsViewModel;
        }
    }
}