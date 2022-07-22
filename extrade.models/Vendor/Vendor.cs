using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public enum VendorStatus :byte { accepted=1,pending,rejected}
    public class Vendor
    {
        public string UserID { get; set; }
        public string BrandNameAr{ get; set; }
        public string BrandNameEr { get; set; }
        public VendorStatus VendorStatus { get; set; }
        public bool IsDeleted { get; set; }
        public float Balance { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<VendorImage> VendorImage{ get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
