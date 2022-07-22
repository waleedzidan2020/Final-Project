using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{



    public static partial class CategoryExtention { 
    
    public static CategoryViewModel ToviewModel (this Category model) {

            return new CategoryViewModel()
            {

                CatID = model.ID,
                NameAr = model.NameAr,

                NameEn = model.NameEn,
                Image = model.Image              

            };

        }


        public static CategoryEditViewModel ToEditViewModel(this CategoryViewModel model)
        {


            return new CategoryEditViewModel()
            {

               ID=model.CatID,
               NameAr=model.NameAr,
               NameEn=model.NameEn,
               Image=model.Image







            };
        }




    }
    public class CategoryViewModel
    {
        
        public int CatID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Image { get; set; }
        public List<string> NameProductsAr { get; set; }
        public List<string> NameProductsEn { get; set; }
        public List<float> Price { get; set; }
        public List<int> Quantity { get; set; }
        public List<string> Description { get; set; }


    }
}
