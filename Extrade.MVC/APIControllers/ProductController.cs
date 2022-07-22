using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.Controler
{
    public class ProductController : Controller
    {
        private readonly ProductRepository ProductRep;
        private readonly UnitOfWork unitOfWork;
        public ProductController(ProductRepository ProductRep, UnitOfWork unitOfWork)
        {
            this.ProductRep= ProductRep;
            this.unitOfWork = unitOfWork;
        }
        [Authorize("Admin"),Authorize("Vendor")]
        [Route("ProductMvc/Get")]
        public IActionResult Get(
                        int ID = 0,
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
                ProductRep.GetProduct( ID,
                         NameEn ,
                         NameAr,
                        Price,
                        CategoryName,
                        ModifiedDate,
                        OrderBy,
                        IsAscending,
                        PageIndex ,
                        PageSize );

            return View("Get",query.Data);
        }
        [Authorize(Roles ="User")]
        [Route("Api/Getproduct")]
        [HttpGet]
        public APIViewModel GetForUsers(
                        
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
                ProductRep.GetProductForUsers(
                         NameEn,
                         NameAr,
                        Price,
                        CategoryName,
                        ModifiedDate,
                        OrderBy,
                        IsAscending,
                        PageIndex,
                        PageSize);

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = query
            };
        }
        //[HttpGet]
        //public APIViewModel GetProductWithCollection(string CollectionCode)
        //{
        //    var result = ProductRep.GetProductForCollection();
        //    var final= result.Select(p => new ProductViewModel
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
        [Route("Mvc/SearchProudct")]
        public IActionResult Search(int PageIndex,int PageSize)  /////// pagination for product in mvc
        {
            var Data=ProductRep.Search(PageIndex, PageSize);
            return View(Data);
        }

        [HttpGet]
        [Authorize("Vendor")]
        [Route("Mvc/AddProduct")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductEditViewModel model)
        {
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
            model.VendorID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Price *= 10 / 100;
            ProductRep.Add(model.ToModel());
            unitOfWork.Submit();
            return RedirectToAction("");
        }
        [HttpGet]
        public APIViewModel Update(int id)
        {
           var result= ProductRep.GetProductByID(id);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = result
            };
        }
        [HttpPost]
        public APIViewModel Update(ProductEditViewModel obj)
        {
            string Upload = "/Content/Uploads/ProductImage/";
            obj.Images = new List<string>();
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
            ProductRep.Update(obj.ToModel());
            unitOfWork.Submit();
            return new APIViewModel
            {
                 Success=true,
                 Massege="",
                 Data=null
            };
        }
        public IActionResult Delete(int ID)
        {
            ProductRep.Delete(ID);
            unitOfWork.Submit();
            return null;
        }
        public IActionResult AcceptProduct(int ID)
        {
            ProductRep.AcceptProduct(ID);
            unitOfWork.Submit();
            return RedirectToAction();
        }
        public IActionResult RejectProduct(int ID)
        {
            ProductRep.RejectProduct(ID);
            unitOfWork.Submit();
            return null;
        }
        
    }
}
