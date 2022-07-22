using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Microsoft.AspNetCore.Http;

namespace Extrade.ViewModels
{


    public static partial class VendorExtension
    {

        public static Vendor ToModel(this VendorEditViewModel model)
        {


            return new Vendor()
            {
                BrandNameAr = model.BrandNameAr,
                BrandNameEr = model.BrandNameEr,
                VendorImage = model.VendorImage.Select(p => new VendorImage() {
                    Image = p
                }).ToList(),
               VendorStatus=model.VendorStatus,
                UserID = model.UserId,
                Balance = model.Balance,

            };


        }

    }
    public class VendorEditViewModel
    {
        public float Balance { get; set; }

        public string? BrandNameAr { get; set; }
        public string? BrandNameEr { get; set; }
        public string? UserId { get; set; }
        public List<string>? VendorImage { get; set; }
        public VendorStatus VendorStatus { get; set; }
        public IFormFileCollection? ImageFile { get; set; }
        public bool IsDeleted { get; set; }


    }
}
