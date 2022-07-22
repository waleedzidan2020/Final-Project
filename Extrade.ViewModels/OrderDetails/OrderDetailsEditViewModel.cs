using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public static partial class OrderDetailsExtension
    {


        public static OrderDetails ToModel(this OrderDetailsEditViewModel model)
        =>
            new OrderDetails()
            {
                ID=model.ID,
                SubPrice = model.SubPrice,
                OrderId = model.OrderID,
                ProductId = model.ProductID,
                Quantity = model.ProductQuantity
            };
        
    }
    public class OrderDetailsEditViewModel
    {
            public int ID { get; set; }
            public int OrderID { get; set; }
            public int ProductID { get; set; }
            public float SubPrice { get; set; }
            public int ProductQuantity { get; set; }
            public float TotalPrice { get; set; }

    }
    
}
