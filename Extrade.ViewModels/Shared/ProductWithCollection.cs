//using extrade.models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Extrade.ViewModels
//{
//    public class ProductWithCollection
//    {
//    }


//    public class ProductViewModel
//    {
//        public int ID { get; set; }
//        public int CategoryID { get; set; }
//        public string? VendorID { get; set; }
//        public string? NameEn { get; set; }
//        public string? NameAr { get; set; }
//        public string? Description { get; set; }
//        public float Price { get; set; }
//        public ProductStatus Status { get; set; }
//        public string? Category { get; set; }
//        public string? VendorName { get; set; }
//        public int Quantity { get; set; }
//        public List<string>? Images { get; set; }


//    }
//}
//public static partial class ProductExtensions
//{
//    public static ProductWithCollection ToViewModel(this Product model)
//    {
//        return new ProductWithCollection
//        {
//            ID = model.ID,
//            VendorID = model.VendorID,
//            CategoryID = model.CategoryID,
//            NameEn = model.NameEn,
//            NameAr = model.NameAr,
//            Description = model.Description,
//            Status = model.Status,
//            Price = model.Price,
//            Quantity = model.Quantity,
//            Images = model.ProductImages.Select(p => p.image).ToList(),
//            Category = model.Category.NameEn,
//            VendorName = model.Vendor.BrandNameEr.ToString(),
//        };
//    }
//}

