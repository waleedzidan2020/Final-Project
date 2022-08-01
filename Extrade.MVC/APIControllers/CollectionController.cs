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
        private MarketerRebository MarkRepo;
        private UnitOfWork UnitOfWork;
        private UserManager<User> UserMan;
        public CollectionController(MarketerRebository marketerRebository,UserManager<User> _Um,UserRepository _userrepo,CollectionRepository _CollectionRepo,
         UnitOfWork _UnitOfWork)
        {
            MarkRepo = marketerRebository;
            UserMan = _Um;
            UserRepo = _userrepo;
            CollectionRepo = _CollectionRepo;
            UnitOfWork = _UnitOfWork;
        }
        
        public APIViewModel Get(string NameAr = "", string? NameEN = "",
            string Namepenroduct = "", string Namearproduct = "",
            string Description = "", float Price = 0, int Quantity = 0, ProductStatus? Status = null,
                string? orderby = null,string UserId = "",
           bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var data =
           CollectionRepo.Get(UserId,NameAr, NameEN,
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
        [HttpGet]
        public APIViewModel GetWhereMarketerID(string id)
        {
           
           var result = CollectionRepo.GetWhereMarketerID(id);
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


        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return null;
        //}
        //[Authorize(Roles = "Marketer")]
        [Route("Api/AddCollection")]
        [HttpPost]
        public async Task< APIViewModel> Add([FromBody]CollectionEditViewModel model)
        {
            var user = UserRepo.GetUserByID(model.MarketerID);
            var mark = MarkRepo.GetOne(model.MarketerID);
            
            if(mark.Status==MarketerStatus.accebted)
            { 
                var check =await UserMan.GetRolesAsync(user);
                for (int i = 0; i < check.Count; i++)
                {
                    if (check[i] == "Marketer")
                    {
                        Guid g = Guid.NewGuid();
                        model.Code = g.ToString().Substring(0, 10);
                        model.MarketerID = model.MarketerID;
                        CollectionRepo.Add(model);
                        UnitOfWork.Submit();

                        var data = CollectionRepo.GetOneByCode(model.Code);
                        return new APIViewModel
                        {

                            Massege = "Done Added",
                            Success = true,
                            Data = data,
                            url = "Api/GetCollection"

                        };

                    }
                    else return new APIViewModel
                    {

                    };
                }
            }
            return new APIViewModel
            {

            };
            
        }
        public APIViewModel GetDetails(int ID)
        {
            var result = CollectionRepo.GetOne(ID);

            return new APIViewModel
            {
                Data = result,
                Success = true,
                Massege = "get"
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




        [HttpPost]
        public APIViewModel Delete([FromBody] CollectionEditViewModel obj)
        {
            try
            {
                CollectionRepo.Delete(obj);
                UnitOfWork.Submit();
                return new APIViewModel
                {
                    Success = true,
                    Massege = "",
                    Data = null
                };
            }catch(Exception e)
            {
                return new APIViewModel
                {
                    Success = false,
                    Massege = "",
                    Data = e.ToString()     
                };
            }
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

