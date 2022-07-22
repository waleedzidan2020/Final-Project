using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;

namespace Extrade.ViewModels
{


    public static partial class OrderExtension { 
    
     public static OrderViewModel ToViewModel (this Order model)
        {


            return new OrderViewModel()
            {

                ID = model.ID,
                UserID=model.UserID,
                status=model.OrderStatus,
                TotalPrice=model.TotalPrice,
                IsDeleted=model.IsDeleted,
                CollectionCode = model.CollectionCode,

            };


        }
    
    
    
    
    }
    public class OrderViewModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public OrderStatus status { get; set; }
        public DateTime Date { get; set; }
        public List<int>? OrderDetails { get; set; }
        public float TotalPrice { get; set; }
        public string? CollectionCode { get; set; } = null;
        public int DriverID { get; set; }
        public bool IsDeleted { get; set; }

    }

}
