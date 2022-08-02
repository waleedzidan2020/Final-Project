using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC
{
    public class ProductController : Controller
    {
        private readonly ProductRepository ProductRep;
        private readonly VendorRepository VendorRep;
        private readonly UnitOfWork unitOfWork;
        private readonly CategoryRepository categoryrepo;


        public ProductController(VendorRepository Vendorrepo,ProductRepository ProductRep, UnitOfWork unitOfWork, CategoryRepository categoryrepo)
        {
            VendorRep=Vendorrepo;
            this.ProductRep= ProductRep;
            this.unitOfWork = unitOfWork;
            this.categoryrepo = categoryrepo;
        }
        //[Authorize("Admin")]
        [Route("ProductMvc/Get")]
        public IActionResult Get(
                        string ID = "",
                        string? NameEn = null,
                        string? NameAr = null,
                        float Price = 0,
                        string? CategoryName = null,
                        DateTime? ModifiedDate = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20)
        {
            var query = 
                ProductRep.GetProductForVendor( ID,
                         NameEn ,
                         NameAr,
                        Price,
                        CategoryName,
                        ModifiedDate,
                        OrderBy,
                        IsAscending,
                        PageIndex ,
                        PageSize );

            return View("Get",query);
        }
        public IActionResult VendorGet(
                        string ID =null,
                        string? NameEn = null,
                        string? NameAr = null,
                        float Price = 0,
                        string? CategoryName = null,
                        DateTime? ModifiedDate = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20)
        {
            ID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query =
                ProductRep.GetProductForVendor(ID,
                         NameEn,
                         NameAr,
                        Price,
                        CategoryName,
                        ModifiedDate,
                        OrderBy,
                        IsAscending,
                        PageIndex,
                        PageSize);

            return View("VendorGet", query);
        }
        #region ss
        //[Authorize(Roles ="User")]
        //[Route("Api/Getproduct")]
        //[HttpGet]
        //public APIViewModel GetForUsers(
        //                int CategoryID = 1,
        //                string? NameEn = null,
        //                string? NameAr = null,
        //                float Price = 0,
        //                string? CategoryName = null,
        //                DateTime? ModifiedDate = null,
        //                string OrderBy = "",
        //                bool IsAscending = false,
        //                int PageIndex = 1,
        //                int PageSize = 20)
        //{
        //    var query =
        //        ProductRep.GetProductForUsers(
        //            CategoryID,
        //                 NameEn,
        //                 NameAr,
        //                Price,
        //                CategoryName,
        //                ModifiedDate,
        //                OrderBy,
        //                IsAscending,
        //                PageIndex,
        //                PageSize);

        //    return new APIViewModel
        //    {
        //        Success = true,
        //        Massege = "",
        //        Data = query
        //    };
        //}
        //[HttpGet]
        //public APIViewModel GetProductWithCollection(string CollectionCode)
        //{
        //    var result = ProductRep.GetProductForCollection();
        //    var final = result.Select(p => new ProductViewModel
        //    {
        //        CollectionCode = CollectionCode,
        //    });

        //    return new APIViewModel
        //    {
        //        Data = final,
        //        Massege = "",
        //        Success = true,
        //        url = ""
        //    };
        //}
        #endregion
        [Route("Mvc/SearchProudct")]
        public IActionResult Search(int PageIndex,int PageSize)  /////// pagination for product in mvc
        {
            var Data=ProductRep.Search(PageIndex, PageSize);
            return View(Data);
        }

        [HttpGet]
        [Authorize(Roles ="Vendor")]
     
        public IActionResult Add() //edit ui 
        {
                
                var cat= categoryrepo.Get();

            ViewBag.list = cat.Data;
            

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Vendor")]
        public IActionResult Add(ProductEditViewModel model)
        {
            model.VendorID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vendor = VendorRep.GetOne(model.VendorID);
            //if (vendor.Status == VendorStatus.accepted)
            //{
                string Upload = "/Content/Uploads/ProductImage/";
                model.Images = new List<string>();
                foreach (IFormFile f in model.uploadedimg)
                {
                    string NewFileName = Guid.NewGuid().ToString() + f.FileName;
                    model.Images.Add(Upload + NewFileName);
                    FileStream fs = new FileStream(Path.Combine(
                    Directory.GetCurrentDirectory(), "Content", "Uploads", "ProductImage", NewFileName
                    ), FileMode.Create);
                    f.CopyTo(fs);
                    fs.Position = 0;
                }
            float discont = model.Price;
          discont = discont * (10f / 100f);
            model.Price += discont;
            model.Status = ProductStatus.pending;
                ProductRep.Add(model.ToModel());
                unitOfWork.Submit();
                return RedirectToAction("VendorGet");
            //}
            //else return RedirectToAction("VendorGet");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
           var result= ProductRep.GetProduct(id);
            ViewBag.id = result.VendorID;
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(ProductEditViewModel obj)
        {
            var catid = ProductRep.GetCategoryId(obj.ID);
            string Upload = "/Content/Uploads/ProductImage/";
            obj.Images = new List<string>();
            if (obj.uploadedimg != null)
            {
                foreach (IFormFile f in obj.uploadedimg)
                {
                    string NewFileName = Guid.NewGuid().ToString() + f.FileName;
                    obj.Images.Add(Upload + NewFileName);
                    FileStream fs = new FileStream(Path.Combine(
                    Directory.GetCurrentDirectory(), "Content", "Uploads", "ProductImage", NewFileName
                    ), FileMode.Create);
                    f.CopyTo(fs);
                    fs.Position = 0;
                }
            }
            obj.Status = ProductStatus.pending;
       
            obj.CategoryID = catid;
            ProductRep.Update(obj);
            unitOfWork.Submit();
            return RedirectToAction("VendorGet");
        }
        [Authorize(Roles = "Vendor")]
        public IActionResult DeleteVendor(int ID)
        {
            ProductRep.Delete(ID);
            unitOfWork.Submit();
            return RedirectToAction("VendorGet");
        }



        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int ID)
        {
            ProductRep.Delete(ID);
            unitOfWork.Submit();
            return RedirectToAction("Get");
        }
        public IActionResult AcceptProduct(int ID)
        {
            var x = ProductRep.ProductStatus(ID);
            unitOfWork.Submit();
            return RedirectToAction("Get");
        }
        //public IActionResult RejectProduct(int ID)
        //{
        //    ProductRep.RejectProduct(ID);
        //    unitOfWork.Submit();
        //    return null;
        //}
       
    }
}
