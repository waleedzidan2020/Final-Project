
using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using extrade.models;
using Extrade.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Extrade.MVC.Controler
{
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository repo;
        private readonly UnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly UserRepository _user;
        public CategoryController(CategoryRepository repo, UnitOfWork unitOfWork, UserManager<User> _userManager, UserRepository _user)
        {
            this._user = _user;
            this.repo = repo;
            this.unitOfWork = unitOfWork;
            this.userManager = _userManager;
        }
        

        //[Authorize(Roles = "User")]
        [Route("Api/GetCaterory")]
        public  APIViewModel Get(int _ID = 0, string NameAr = "", string NameEn = "", string orderby = null,
            bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {



            var data = repo.Get(_ID, NameAr, NameEn, orderby, IsAsceding, pageindex, pagesize);

            return new APIViewModel
            {
                Success = true,
                Massege="",
                Data=data.Data
            };



        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return null;
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public APIViewModel Add(CategoryEditViewModel model)
        {
            string Uploade = "/Content/Uploads/Category/";
            IFormFile s = model.ImageFile;
            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            model.Image = Uploade + NewFileName;
            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "Category", NewFileName
                ), FileMode.Create);
            s.CopyTo(fs);
            fs.Position = 0;
            repo.Add(model);
            unitOfWork.Submit();
            return null;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("Api/UpdateCaterory")]
        public APIViewModel Update(int Id)
        {
            var res=  repo.GetOne(Id);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = res
            };
            //ViewBag.Id = res.CatID;
            //return null(new CategoryEditViewModel
            //{
            //    ID=res.CatID,
            //    NameAr = res.NameAr,
            //    NameEn = res.NameEn,
            //    Image = res.Image
            //});
            
            return null;

        }




        [Authorize(Roles = "admin")]
        public IActionResult Remove(int Id)
        {
            var res = repo.GetOne(Id);
            
            //ViewBag.Id = res.CatID;
            repo.Remove(res.ToEditViewModel());
            unitOfWork.Submit();
            return RedirectToAction("Get");

        }
        //[HttpGet]
        //public IActionResult Update()
        //{
        //    return View();
        //}

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Update(CategoryEditViewModel model,int id=0 )
        {
            //get from data
            string Uploade = "/Content/Uploads/Category/";
            IFormFile s = model.ImageFile;
            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            model.Image = Uploade + NewFileName;
            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "Category", NewFileName


            ), FileMode.Create);

            s.CopyTo(fs);
            fs.Position = 0;
            repo.Update(model);
            unitOfWork.Submit();
            return null;
        }

    }
}