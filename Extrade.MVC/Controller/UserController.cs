using extrade.models;
using Extrade.MVC.Helpers;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Dynamic;
using System.Security.Claims;

namespace Extrade.MVC
{
    //[Route("User")]
    //[ApiController]
    //[Produces("application/json")]
    public class UserController : Controller
    {
        public readonly ExtradeContext DBContext;
        public readonly UnitOfWork unit;
        public readonly UserRepository UserRep;
        public readonly RoleRepository role;
        public readonly UserManager<User> manager;


        public UserController(UserRepository _UserRep, UnitOfWork _unit, ExtradeContext Context, RoleRepository role, UserManager<User> manager)
        {
            DBContext = Context;
            unit = _unit;
            UserRep = _UserRep;
            this.role = role;
            this.manager = manager;
        }
        [Route("index")]

        [HttpGet("Name")]
        public IActionResult index()
        {
            dynamic x = new ExpandoObject();
            x.hi = "Hello World";
            return new ObjectResult(x);
        }

        [Route("Mvc/AllUsers")]
        [Authorize(Roles ="Admin")]
        public IActionResult AllUsers(
                        string? ID = null,
                        string? NameEn = null,
                        string? NameAr = null,
                        string? Country = null,
                        string? City = null,
                        string? Street = null,
                        bool? IsDeleted = null,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20)
        {
            var query =
                UserRep.Get(ID,
                         NameEn,
                         NameAr,
                        Country,
                        City,
                        Street,
                        IsDeleted,
                        OrderBy,
                        IsAscending,
                        PageIndex,
                        PageSize);

            return View(query);
        }
        public IActionResult Index()
        {
            return null;
        }
        [Route("SignInMvc")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();

        }
        [Route("SignInMvc")]
        [HttpPost]

        public async Task<IActionResult> SignIn( UserLoginViewModel obj)
        {
       
            var user = UserRep.GetByEmails(obj.Email);
            if (user != null) {

             
               
                    var result = await UserRep.SignInAsMVc(obj);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Wrong Email or Password !!");
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Sorry, Please Try again Later ");
                    }
                    else
                    {
                        if (manager.GetRolesAsync(user).Result.FirstOrDefault() == "Vendor") { return RedirectToAction("Add", "Vendor"); }

                        else if (manager.GetRolesAsync(user).Result.FirstOrDefault() == "Admin") { return RedirectToAction("AllUsers", "User"); }


                    }











            } else
            {
                ModelState.AddModelError("", "Enter Your Email And Password");
                return View();
            }

            return View();
         
        }
        [HttpGet]
       [Route("Mvc/SignOut")]
        public new async Task<IActionResult> SignOut()
        {
            await UserRep.SignOut();
            return View("SignIn");
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return null;
        }
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] UserControllersViewModel obj)
        {
            string Uploade = "/Content/Uploads/UserImage/";
            IFormFile? s = obj.uploadedimg;
            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            obj.Img = Uploade + NewFileName;
            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
                ), FileMode.Create);
            s.CopyTo(fs);
            fs.Position = 0;
            obj.Role = role.GetUserRole().Text;
            var result =
            await UserRep.Add(obj);
            if (result.Succeeded)
            {
                return null;
            }
            else return null;
        }

        public APIViewModel Update(string ID)
        {
            var obj = UserRep.GetByID(ID);
            return new APIViewModel()
            {
                Success = true,
                Massege = "",
                Data = obj
            };
        }

        public async Task<APIViewModel> edit([FromBody] UserControllersViewModel obj)
        {
            string Uploade = "/Content/Uploads/UserImage/";

            IFormFile? s = obj.uploadedimg;


            string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            obj.Img = Uploade + NewFileName;


            FileStream fs = new FileStream(Path.Combine(
                Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
                ), FileMode.Create);

            s.CopyTo(fs);
            fs.Position = 0;
            await UserRep.Update(obj);
            unit.Submit();
            return new APIViewModel
            {
                Data = null,
                Success = true,
                Massege = "Done"
            };
        }


        //[Authorize(Roles ="admin")]
        public async Task<IActionResult> SoftDelete(string ID)
        {
            await UserRep.Delete(ID);
            unit.Submit();
            return RedirectToAction("AllUsers");
        }
        public async Task<IActionResult> UpdateRole(string ID)
        {
            await UserRep.AddRoleToUser(ID);
            return RedirectToAction("AllUsers", "User");
        }
    }
}
