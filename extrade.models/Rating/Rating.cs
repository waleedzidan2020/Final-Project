using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class Rating
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public float Value { get; set; } 
        public string Comment { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }





    }
}
