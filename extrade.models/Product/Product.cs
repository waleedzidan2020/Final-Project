using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
        
    public enum ProductStatus : byte { accepted = 1, pending = 2, rejected = 3 }
    public class Product
    {
        public int ID { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public int CategoryID { get; set; }
        
        public string VendorID { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Category Category { get; set; }
        public virtual Vendor Vendor { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<CollectionDetails> CollectionDetails { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        
    }
}
