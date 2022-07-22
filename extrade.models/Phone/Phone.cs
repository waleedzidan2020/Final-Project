using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace extrade.models
{
    public class Phone
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Number { get; set; }
        public virtual User User { get; set; }


    }
}
