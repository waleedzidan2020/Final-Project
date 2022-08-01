using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;



namespace Extrade.ViewModels
{



    public static partial class OrderExtension {


        public static Order ToModel(this OrderEditViewModel model)
        {
            return new Order()
            {
                ID = model.ID,
                UserID=model.UserID,
                OrderStatus = model.status,
                TotalPrice = model.TotalPrice,
                IsDeleted = model.IsDeleted,
                DriverID=model.DriverID,
                CollectionCode=model.CollectionCode
            };


        }


    }
    public class OrderEditViewModel
    {
        public int ID { get; set; }
        public int? DriverID { get; set; }
        public string? UserID { get; set; }
        public OrderStatus status { get; set; }
        public float TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public string? CollectionCode { get; set; } = null;
    }
}
