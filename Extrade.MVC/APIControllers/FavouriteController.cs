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
        public FavouriteController(UnitOfWork _unitOfWork,FavouriteRepository _FavRepo)
        {
            unitOfWork = _unitOfWork;
            FavRepo = _FavRepo;
        }


        public APIViewModel Get(
                        int ID = 0,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20
            )
        {
            string LoginUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = FavRepo.GetFavourites(LoginUser, ID = 0, OrderBy = "",
                        IsAscending = false,
                        PageIndex = 1,
                        PageSize = 20);
            
            return new APIViewModel
            {
                Success=true,
                Massege="This is your Favourite  Products",
                Data=result.Data
            };
        }
        [HttpPost]
        public APIViewModel Add(Favourite obj)
        {
            obj.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FavRepo.Add(obj);
            unitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = null
            };
        }
        //public APIViewModel Remove(int ID)
        //{
        //    FavRepo.Remove(ID);
        //    unitOfWork.Submit();
        //    return new APIViewModel
        //    {
        //        Success = true,
        //        Massege = "",
        //        Data = null
        //    };
        //}
    }
}
