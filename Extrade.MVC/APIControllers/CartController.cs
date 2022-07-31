using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.APIControllers
{
    public class CartController : ControllerBase
    {
        private readonly CartRepository CartRepo;
        private readonly UnitOfWork UnitOfWork;
        private readonly ProductRepository productRepo;

        public CartController(CartRepository _CartRepo, UnitOfWork unitOfWork, ProductRepository productrepo)
        {
            CartRepo = _CartRepo;
            UnitOfWork = unitOfWork;
            productRepo = productrepo;
        }
        public IActionResult Index()
        {
            return null;
        }
        [HttpGet]
        public APIViewModel Get(string UserID = "")
        {
            var result = CartRepo.Get(UserID);
            return new APIViewModel
            {
                Success = true,
                Massege = "Products you added to Cart",
                Data = result
            };
        }
        [HttpPost]
        public APIViewModel Add([FromBody] CartEditViewModel Cart)
        {
            var result = CartRepo.Add(Cart);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Added Successfully",
                Data = result
            };
        }
        [HttpPost]
        public APIViewModel Update([FromBody] CartEditViewModel obj)
        {

            var prod = productRepo.GetProductByID(obj.ProductID);
            if(prod.Quantity > obj.Quantity )
            {
                var Result = CartRepo.Update(obj);
                UnitOfWork.Submit();
                return new APIViewModel
                {
                    Success = true,
                    Massege = "Update Done Successfully",
                    Data = Result
                };
            }
            else
            {
                return new APIViewModel
                {
                    Success = false,
                    Massege = "OUT OF STOCK",
                    Data = null
                };
            }
        }
      
        public APIViewModel Remove(int ID)
        {
            var data=CartRepo.GetByID(ID);
            CartRepo.Remove(data);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Deleted Done",
                Data = "Cart/Get"
            };
        }
    }
}
