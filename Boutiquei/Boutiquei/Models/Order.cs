using System;
namespace Boutiquei.Models
{
    public class Order
    {
        public string OrderNumber { set; get; }
        public string OrderDate { set; get; }
        public string Quantity { set; get; }
        public string OrderStatus { set; get; }
        public string OrderTotal { set; get; }

    }
}
