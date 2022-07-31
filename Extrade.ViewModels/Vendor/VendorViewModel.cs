using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;


namespace Extrade.ViewModels
{
    public static partial  class VendorExtension
    {
        public static  VendorViewModel ToViewModel(this Vendor model)
        {
            return new VendorViewModel()
            {
                BrandNameAr = model.BrandNameAr,
                BrandNameEr=model.BrandNameEr,
                VendorImage = model.VendorImage.Select(p => p.Image).ToList(),
                IsDeleted   =model.IsDeleted,
                Balance=model.Balance,
                UserID=model.UserID,    
                Status = VendorStatus.accepted                




            };
        }

        public static VendorEditViewModel ToEditViewModel(this VendorViewModel model)
        {


            return new VendorEditViewModel()
            {

                UserId=model.UserID,
                BrandNameAr = model.BrandNameAr,

                BrandNameEr = model.BrandNameEr,

               Balance= model.Balance,
                VendorImage = model.VendorImage.ToList(),
               IsDeleted =model.IsDeleted
                
                





            };
        }
    }
    public class VendorViewModel
    {
        
        public string? UserID { get; set; }
        public string? BrandNameAr { get; set; }
        public string? BrandNameEr { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public float Balance { get; set; }
        public List<string>? VendorImage { get; set; }
        public List<string>? Product { get; set; }
        public VendorStatus Status{ get; set; }
        public bool IsDeleted { get; set; }


    }
}
