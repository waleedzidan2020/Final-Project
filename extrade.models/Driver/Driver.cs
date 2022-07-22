using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace extrade.models
{
    public enum DriverStatus :byte
    {
        offline=1,
        online=2,
        block=3
    }
    public class Driver
    {
        public int ID { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string DriverLicense { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<PhoneDriver> PhoneDriver { get; set; }
        public virtual ICollection<Order> Order { get; set; }

    }
}
