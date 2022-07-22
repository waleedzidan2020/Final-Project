using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    
    public class User : IdentityUser
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Img { get; set; }
        public bool IsDeleted { get; set; }
        public virtual new ICollection<Phone> PhoneNumber { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Marketer Marketer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }


    }
}
