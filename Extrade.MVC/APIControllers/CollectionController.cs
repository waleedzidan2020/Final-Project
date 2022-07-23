using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.Controler
{
    public class CollectionController : Controller
    {


        private CollectionRepository CollectionRepo;
        private UserRepository UserRepo;
        private UnitOfWork UnitOfWork;
        private UserManager<User> UserMan;
        public CollectionController(UserManager<User> _Um,UserRepository _userrepo,CollectionRepository _CollectionRepo,
         UnitOfWork _UnitOfWork)
        {
            UserMan = _Um;
            UserRepo = _userrepo;
            CollectionRepo = _CollectionRepo;
            UnitOfWork = _UnitOfWork;
        }
        
        public APIViewModel Get(string NameAr = "", string? NameEN = "",
            string Namepenroduct = "", string Namearproduct = "",
            string Description = "", float Price = 0, int Quantity = 0, ProductStatus? Status = null,
                string? orderby = null,
           bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data =
           CollectionRepo.Get(id,NameAr, NameEN,
           Namepenroduct, Namearproduct, Description, Price, Quantity, Status, orderby,
               IsAsceding, pageindex, pagesize);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data
            }; 
            //return View(data.Data);
        }


        [Route("Api/GetCollection")]
        //[Authorize(Roles = "Marketer")]
        public APIViewModel GetWhereMarketerID()
        {
            var ID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = CollectionRepo.GetWhereMarketerID(ID);
         return new APIViewModel
         {
             Success = true,
             Massege = "",
             Data = result
         }; 
        }
        public APIViewModel Search(int pageindex = 1, int pagesize = 20)
        {
            var Data = CollectionRepo.Search(pageindex, pagesize);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = Data
            };
            //return View("Get", Data);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return null;
        }
        //[Authorize(Roles = "Marketer")]
        [Route("Api/AddCollection")]
        [HttpPost]
        public APIViewModel Add([FromBody]CollectionEditViewModel model)
        {
            var user = UserRepo.GetUserByID(model.MarketerID);
            var check = UserMan.GetRolesAsync(user);
            for (int i = 0; i < check.Result.Count; i++)
            {
                if (check.Result[i] == "Marketer") { 
                Guid g = Guid.NewGuid();
                model.Code = g.ToString().Substring(0, 10);
                model.MarketerID = model.MarketerID;
                CollectionRepo.Add(model);
                UnitOfWork.Submit();
                return new APIViewModel
                {

                    Massege = "Done Added",
                    Success = true,
                    Data = null,
                    url = "Api/GetCollection"

                };
            }
                else return new APIViewModel
                {

                };
            };
             return new APIViewModel
            {

            };
        }

        public IActionResult ProductWithCollection(int ID)
        {
            var result = CollectionRepo.GetOne(ID);
            return RedirectToAction("GetProductWithCollection", "Product", result.ID);
        }
        [HttpGet]
        public APIViewModel Update(int id = 0)
        {
            var ids =
                    CollectionRepo.GetOne(id);
                
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = ids
            };
        }
        [HttpPost]

        public IActionResult Update(CollectionEditViewModel model)
        {
            return null;
        }






        //public IActionResult Remove()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Remove(CollectionEditViewModel model)
        //{
        //    return 0;
        //}

    
}
}

