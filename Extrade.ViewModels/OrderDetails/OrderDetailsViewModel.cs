using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;

namespace Extrade.ViewModels
{
    public static partial class OrderDetailsExtension
    {

        public static OrderDetailsViewModel ToViewModel(this OrderDetails model)=>
        
            new OrderDetailsViewModel
            {
                ID = model.ID,
                OrderID = model.OrderId,
                ProductID = model.ProductId,
                ProductQuantity =model.Quantity,
                SubPrice= model.SubPrice,
                
            };
        
    }
    public class OrderDetailsViewModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float SubPrice { get; set; }
        public int ProductQuantity { get; set; }
       
        public float TotalPrice { get; set; }



    }
}
