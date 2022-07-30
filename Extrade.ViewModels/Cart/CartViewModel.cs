using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class CartViewModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public int ProductID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public int Quantity { get; set; }
      
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Images { get; set; }

    }
}
