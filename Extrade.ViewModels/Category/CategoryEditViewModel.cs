using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Microsoft.AspNetCore.Http;

namespace Extrade.ViewModels
{


    public static partial class CategoryExtension {

        public static Category ToModel(this CategoryEditViewModel model) {


            return new Category()
            {
                NameAr=model.NameAr,
                NameEn=model.NameEn,
                Image=model.Image
            };
        
        
        }
    
    
    }
    public class CategoryEditViewModel
    {
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
