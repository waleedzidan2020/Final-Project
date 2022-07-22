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
        public CartController(CartRepository _CartRepo,UnitOfWork unitOfWork)
        {
            CartRepo= _CartRepo;
            UnitOfWork= unitOfWork;
        }
        public IActionResult Index()
        {
            return null;
        }
        [HttpGet]
        public APIViewModel Get()
        {
            var result = CartRepo.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return new APIViewModel
            {
                Success = true,
                Massege = "Products you added to Cart",
                Data = result
            };
        }
        [HttpPost]
        public APIViewModel Add(CartEditViewModel Cart)
        {
            Cart.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        public APIViewModel Update(CartEditViewModel obj)
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
        [HttpPost]
        public APIViewModel Remove(int ID)
        {
            var result = CartRepo.Remove(ID);
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
