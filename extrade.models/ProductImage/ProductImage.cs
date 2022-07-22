using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class ProductImage
    {
        public int ProductID { get; set; }
        public string image { get; set; }
        public virtual Product Product { get; set; }
    }
}
