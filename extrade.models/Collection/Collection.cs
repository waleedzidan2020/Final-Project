using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class Collection
    {
        public int ID { get; set; }
        public string MarketerID { get; set; }
        public string NameAr { get; set; }
        public string NameEN { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual Marketer Marketer { get; set; }
        public virtual ICollection<CollectionDetails> CollectionDetails { get; set; }
    }
}
