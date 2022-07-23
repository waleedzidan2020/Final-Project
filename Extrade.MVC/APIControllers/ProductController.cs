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
        private readonly UnitOfWork unitOfWork;
        public ProductController(ProductRepository ProductRep, UnitOfWork unitOfWork)
        {
            this.ProductRep= ProductRep;
            this.unitOfWork = unitOfWork;
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
       
       
        
    }
}
