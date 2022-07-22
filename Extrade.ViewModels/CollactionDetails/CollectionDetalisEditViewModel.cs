using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
   
        public static partial class CollectionExtensions
        {
            public static CollectionDetails ToModel(this CollectionDetalisEditViewModel model)
            {
                return new CollectionDetails
                {
                    ProductID = model.ProductID,
                    CollectionID = model.CollectionID,
                };

            }
        }
    public class CollectionDetalisEditViewModel
    {
        public int ProductID { get; set; }
        public int CollectionID { get; set; }

        public List<CollectionViewModel> Collection { get; set; }

        public List<Product> Product { get; set; }
    }
}
