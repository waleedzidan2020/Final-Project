using extrade.models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public static partial class ProductExtensions
    {
        public static Product ToModel(this ProductEditViewModel model)
        {
            return new Product
            {
                ID = model.ID,
                VendorID = model.VendorID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                CategoryID=model.CategoryID,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Status= model.Status,
                ProductImages=model.Images.Select(p=>new ProductImage
                {
                    image=p
                }).ToList(),
                // Category = model.Category.NameEn.ToString(),
                //Vendor = model.VendorName.Select(i => i.User.UserName),

            };
        }
        public static ProductEditViewModel ToEditModel(this Product model)
        {
            return new ProductEditViewModel
            {
                ID = model.ID,
                VendorID = model.VendorID,
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                CategoryID = model.CategoryID,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Status = model.Status,
               
                // Category = model.Category.NameEn.ToString(),
                //Vendor = model.VendorName.Select(i => i.User.UserName),

            };
        }
    }
    
    public class ProductEditViewModel
    {
        public int ID { get; set; }
        public string? VendorID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public extrade.models.ProductStatus Status { get; set; }
        public int CategoryID { get; set; }
        public string? Category { get; set; }
        public string? VendorName { get; set; }
        public int Quantity { get; set; }
        
        public List<string>? Images { get; set; }
        public IFormFileCollection? uploadedimg { get; set; }


    }
}
