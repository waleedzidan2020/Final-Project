
using extrade.models;
using Extrade.MVC.Helpers;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Security.Claims;

namespace Extrade.MVC.Controler
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
        public IActionResult Register()
        {
            
            ViewBag.Roles = role.GetVendorRole().Text;
            return View();
        }
        public async Task<IActionResult> Create(UserControllersViewModel obj)
        {
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
                return RedirectToAction("SignIn");
            }
            else return View("Register");
        }



        public async Task<IActionResult> edit(UserControllersViewModel obj)
        {
            string Uploade = "/Content/Uploads/UserImage/";

            IFormFile s = obj.uploadedimg;


            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            obj.Img = Uploade + NewFileName;


            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
                ), FileMode.Create);

            s.CopyTo(fs);
            fs.Position = 0;
            await UserRep.Update(obj);
            unit.Submit();
            return RedirectToAction("AllUsers");
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
