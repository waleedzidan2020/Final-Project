using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.Controler
{
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository ProductRep;
        private readonly VendorRepository vendorRep;
        private readonly UnitOfWork unitOfWork;
        public ProductController(ProductRepository ProductRep, UnitOfWork unitOfWork, VendorRepository vendorRepository)
        {
            this.ProductRep= ProductRep;
            this.unitOfWork = unitOfWork;
            this.vendorRep= vendorRepository;
        }
       
        //[Authorize(Roles ="User")]
        [Route("Api/Getproduct")]
        [HttpGet]
        public APIViewModel GetForUsers(
                        int categoryID=0,
                        string vendorID="",
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
                        categoryID,
                        vendorID,
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
        [Route("Api/GetBrands")]
        public APIViewModel GetAllBrands()
        {
            var data = vendorRep.GetAllBrands();

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data.Data
            };



        }

        [Route("ProductDetails")]
       [HttpGet]
        public APIViewModel GetProductDetails(int ID)
        {
            var data = ProductRep.GetProductByID(ID);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data
            };
        }
        [HttpGet]
        public APIViewModel GetProductWithCollection(List<CollectionDetalisEditViewModel> CollectionDetailsCode)
        {
            List<int> prodid = new List<int>();
            foreach(var i in CollectionDetailsCode) {
                prodid.Add(i.ProductID);
            }
            var result = ProductRep.GetProductForCollection(prodid);
            for(var x = 0; x < CollectionDetailsCode.Count; x++) { 
                foreach(var i in result)
                {
                    i.CollectionID = CollectionDetailsCode[x].CollectionID;
                }

            }
            return new APIViewModel
            {
                Data = result,
                Massege = "",
                Success = true,
                url = ""
            };
        }
        //[Route("Mvc/SearchProudct")]
        

       
        //public IActionResult RejectProduct(int ID)
        //{
        //    ProductRep.RejectProduct(ID);
        //    unitOfWork.Submit();
        //    return null;
        //}
        //    return new APIViewModel
        //    {
        //        Data = final,
        //        Massege = "",
        //        Success = true,
        //        url = ""
        //    };
        //}
       
       
        
    }
}
