using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public enum MarketerStatus : byte
    {
        accebted=1,
        pending=2,
        rejected=3
    }
    public class Marketer
    {
        
        public string UserID { get; set; }
        public string TaxCard { get; set; }
        public float Salary { get; set; }
        public MarketerStatus MarketerStatus { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
        
        
    }
}
