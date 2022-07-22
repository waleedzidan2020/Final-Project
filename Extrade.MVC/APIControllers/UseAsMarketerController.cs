using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Extrade.MVC.Controler
{
    public class UseAsMarketerController : Controller
    {
        public readonly ExtradeContext DBContext;
        public readonly UnitOfWork unit;
        public readonly UserRepository UserRep;
        public readonly RoleRepository role;

        public UseAsMarketerController(UserRepository _UserRep, UnitOfWork _unit, ExtradeContext Context,
            RoleRepository role)
        {
            DBContext = Context;
            unit = _unit;
            UserRep = _UserRep;
            this.role = role;
        }
        

       

        [Route("/Register/Marketer")]
        public IActionResult Register()
        {
            
            return View();
        }
        [Route("Marketer/Register")]
        public async Task<APIViewModel> Create([FromBody]UserControllersViewModel obj)
        {
            //string Uploade = "/Content/Uploads/UserImage/";
            //IFormFile s = obj.uploadedimg;
            //string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            //obj.Img = Uploade + NewFileName;
            //FileStream fs = new FileStream(Path.Combine(
            //    Directory.GetCurrentDirectory(), "Content", "Uploads", "Marketer", NewFileName
            //    ), FileMode.Create);
            //s.CopyTo(fs);
            //fs.Position = 0;
            obj.Role = role.GetMarketerRole().Text;
            var result =
            await UserRep.Add(obj);
            if (result.Succeeded)
            {
                return new APIViewModel
                {
                    Success = true,
                    url = "User/Login",
                    Massege = "",
                    Data = null
                };
            }
            else return new APIViewModel
            {
                Massege = "Wrong Information",
                Success = false,
                url = "Register",
                Data = null
            };
        }
    }
}
