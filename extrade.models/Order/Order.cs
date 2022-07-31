using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public enum OrderStatus :byte
    {
        accepted=1,
        pending,
        rejected
    }
    public class Order 
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int? DriverID { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool IsDeleted { get; set; }
        public float TotalPrice { get; set; }
        public string CollectionCode { get; set; }
        public DateTime ModifiedDate { get; set; } 
        public virtual User User { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Collection Collection { get; set; }
    }
}
