using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.APIControllers
{
    public class RatingController : Controller
    {
        private readonly RatingRepository RatingRepo;
        private readonly UnitOfWork UnitOfWork;
        private readonly ProductRepository productRepository;
        public RatingController(ProductRepository _productRepository ,RatingRepository _RatingRepo, UnitOfWork unitOfWork)
        {
            productRepository = _productRepository;
            RatingRepo = _RatingRepo;
            UnitOfWork = unitOfWork;
        }
        public APIViewModel GetBestRating(int val = 4)
        {
            var result =RatingRepo.GetBestRating(val);
            var Products = new List<ProductViewModel>();
            for(int i = 0; i < result.Count; i++)
            {
                var finalresult=
                    productRepository.GetProductByID(result[i].ProductID);
                finalresult.Rating = result[i].Value;
                Products.Add(finalresult);
            }
                

            return new APIViewModel
            {
                Success = true,
                Massege = "Top Rated in our Site",
                Data = Products
            };
        }
        public APIViewModel Add(Rating rating)
        {
            var loginUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            rating.UserID = loginUser;
            
            RatingRepo.Add(rating);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Thanks for Rating our Product",
                Data = null
            };
        }
        public APIViewModel Update(Rating rating)
        {
            var loginUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            rating.UserID = loginUser;
            RatingRepo.Update(rating);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Thanks for Rating our Product",
                Data = null
            };
        }
    }
}
