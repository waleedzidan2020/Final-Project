using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class CollectionDetails
    {
        public int ProductID { get; set; }
        public int CollectionID { get; set; }
        public virtual Collection Collection { get; set; }
        public virtual Product Product { get; set; }
    }
}
