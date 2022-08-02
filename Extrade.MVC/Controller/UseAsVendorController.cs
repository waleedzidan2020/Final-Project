
using extrade.models;
using Extrade.MVC.Helpers;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Security.Claims;

namespace Extrade.MVC
{
    public class UseAsVendorController : Controller
    {
        public readonly ExtradeContext DBContext;
        public readonly UnitOfWork unit;
        public readonly UserRepository UserRep;
        public readonly RoleRepository role;

        public UseAsVendorController(UserRepository _UserRep, UnitOfWork _unit, ExtradeContext Context, RoleRepository role)
        {
            DBContext = Context;
            unit = _unit;
            UserRep = _UserRep;
            this.role = role;
        }


      

        [Route("/Register/Vendor")]
        [HttpGet]
        public IActionResult Register()
        {
            
            ViewBag.Roles = role.GetVendorRole().Text;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserControllersViewModel obj)
        {
            obj.IsDeleted = true;
            string Uploade = "/Content/Uploads/UserImage/";
            IFormFile s = obj.uploadedimg;
            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            obj.Img = Uploade + NewFileName;
            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "Vendor", NewFileName
                ), FileMode.Create);
            s.CopyTo(fs);
            fs.Position = 0;
            var result =
            await UserRep.Add(obj);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn","User");
            }
            else return View("Register/Vendor");
        }


        [HttpGet]
        [Route("/Vendor/Edit")]
        public IActionResult edit()
        {
            var userinfo = UserRep.GetByID(User.FindFirstValue(ClaimTypes.NameIdentifier));
           var result= userinfo.ChangeUserToUserControllersViewModel();
        
            return View(result);
        }
        [HttpPost]
        [Route("/Vendor/Edit")]
        public async Task<IActionResult> edit(UserControllersViewModel obj)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            obj.ID = userid;
            string Uploade = "/Content/Uploads/UserImage/";

            IFormFile s = obj.uploadedimg;

            if (obj.uploadedimg != null)
            {
                string NewFileName = Guid.NewGuid().ToString() + s.FileName;
                obj.Img = Uploade + NewFileName;


                FileStream fs = new FileStream(Path.Combine(
                    Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
                    ), FileMode.Create);

                s.CopyTo(fs);
                fs.Position = 0;
            }
            await UserRep.Update(obj);
            unit.Submit();
            return RedirectToAction("VendorGet","Product");
        }


       


        [HttpGet]

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            model.id = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var res = await UserRep.ChangePassword(model);
            if (res.Succeeded)
                ModelState.Clear();
            
            return RedirectToAction("SignIn");
        }




    }



}
