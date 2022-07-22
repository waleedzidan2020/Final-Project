using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class PhoneDriver
    {

        public string Number { get; set; }
        public int DriverID { get; set; }

        public virtual Driver Driver { get; set; }



    }
}
