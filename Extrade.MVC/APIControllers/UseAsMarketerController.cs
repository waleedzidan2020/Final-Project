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
        public readonly MarketerRebository marketer;

        public UseAsMarketerController(UserRepository _UserRep, UnitOfWork _unit, ExtradeContext Context,
            RoleRepository role, MarketerRebository marketer)
        {
            DBContext = Context;
            unit = _unit;
            UserRep = _UserRep;
            this.role = role;
            this.marketer = marketer;
        }




        [Route("/Register/Marketer")]
        public IActionResult Register()
        {
            
            return View();
        }
        [Route("Marketer/Register")]
        public async Task<APIViewModel> Create([FromBody]UserMarketerEditViewModel obj)
        {
           
            obj.Img = "not provided jpg";
            obj.Role = role.GetMarketerRole().Text;
            var result =
            await marketer.Add(obj);
            unit.Submit();
            if (!string.IsNullOrEmpty( result.UserID))
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
