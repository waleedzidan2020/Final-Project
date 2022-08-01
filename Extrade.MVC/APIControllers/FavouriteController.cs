using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.APIControllers
{
    public class FavouriteController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly FavouriteRepository FavRepo;
        private readonly ProductRepository productRepo;
        public FavouriteController(UnitOfWork _unitOfWork,FavouriteRepository _FavRepo, ProductRepository productrepo)
        {
            unitOfWork = _unitOfWork;
            FavRepo = _FavRepo;
            productRepo = productrepo;
        }


        public APIViewModel Get(
                        string UserID = "",
                        int ID = 0,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20
            )
        {
            var result = FavRepo.GetFavourites(UserID, ID, OrderBy,IsAscending,PageIndex,PageSize);
            
            return new APIViewModel
            {
                Success=true,
                Massege="This is your Favourite  Products",
                Data=result.Data
            };
        }
        [HttpPost]
        public APIViewModel Add([FromBody] Favourite fav)
        {
            FavRepo.Add(fav);
            unitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = null
            };
        }
        [HttpPost]
        public APIViewModel Remove(int ID)
        {

            FavRepo.Remove(ID);
            unitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = null
            };
        }
    }
}
