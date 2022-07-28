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

namespace Extrade.MVC.Controler
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
        public readonly UserManager<User> UserManager;

        public UserController(UserManager<User> userManager,UserRepository _UserRep,UnitOfWork _unit, ExtradeContext Context, RoleRepository role)
        {
            UserManager = userManager;
            DBContext = Context;
            unit = _unit;
            UserRep = _UserRep;
            this.role = role;
        }
        [Route("index")]
        [HttpGet("Name")]
        public IActionResult index()
        {
            dynamic x = new ExpandoObject();
            x.hi = "Hello World";
            return new ObjectResult(x);
        }
        //[Route("AllUsers")]
        ////[Authorize(Roles ="admin")]
        //public IActionResult AllUsers(
        //                string? ID = null,
        //                string? NameEn = null,
        //                string? NameAr = null,
        //                string? Country = null,
        //                string? City = null,
        //                string? Street = null,
        //                bool? IsDeleted=null,
        //                string OrderBy = "",
        //                bool IsAscending = false,
        //                int PageIndex = 1,
        //                int PageSize = 20)
        //{
        //    var query =
        //        UserRep.Get(ID,
        //                NameEn,
        //                NameAr,
        //                Country,
        //                City,
        //                Street,
        //                IsDeleted,
        //                OrderBy,
        //                IsAscending,
        //                PageIndex,
        //                PageSize);

        //    return View(query.Data);
        //}
        //public IActionResult Index()
        //{
        //    return null;
        //}
        //[HttpGet]
        //public APIViewModel SignIn(string? returnUrl)
        //{ 
        //    return new APIViewModel
        //    {
        //        url="User/SignIn",

        //    };
        //}
        [HttpPost]
       
        public async Task<ObjectResult> SignIn([FromBody] UserLoginViewModel obj, string? returnUrl = null)
        {
            var user = UserRep.GetByEmail(obj.Email);
            if (user != null)
            {
                if (user.IsDeleted == false)
                {
                    var result = await UserRep.SignIn(obj);
                    if (string.IsNullOrEmpty(result))
                    {
                        return new ObjectResult(new
                        {
                            Success = false,
                            Token = "",
                            ReturnUrl = "",
                            message = "",
                            id = "",
                            RememberMe = "",
                            Role = ""

                        });
                    }
                    else
                    {
                        var u = UserRep.GetByEmails(obj.Email);
                        var role = UserManager.GetRolesAsync(u).Result.FirstOrDefault();
                       
                        return new ObjectResult(new
                        {
                            Success=true,
                            Token = result,
                            ReturnUrl = returnUrl,
                            message = "Done",
                            id= user.ID,
                            RememberMe = obj.RememberMe.ToString(),
                            Role=role

                        });
                    }

                }
                else { 
                  List<string> errors = new List<string>();
                foreach (var i in ModelState.Values)
                    foreach (var e in i.Errors)
                        errors.Add(e.ErrorMessage);

                    return new ObjectResult(new
                    {
                        Token="",
                        Success=false,
                        ReturnUrl = returnUrl,
                        message = "EmailIsDeleted",
                        //RememberMe = obj.RememberMe.ToString()




                    });


                }
                 

            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var i in ModelState.Values)
                    foreach (var e in i.Errors)
                        errors.Add(e.ErrorMessage);
                return new ObjectResult(new
                        {
                            Token = "",
                            ReturnUrl = returnUrl,
                            message = "NotFound",
                            RememberMe = obj.RememberMe.ToString()




                        });
            }
            return null;
        }




        [HttpGet]
        public new async Task<APIViewModel> SignOut()
        {
            await UserRep.SignOut();
            return new APIViewModel
            {
                url = "User/Login",
                Massege = "Please Login",
                Success = true,
                Data = null
            };
        }
        //public IActionResult Register()
        //{
        //    return null;
        //}
        [HttpPost]
        [Route("User/Register")]
        public async Task<APIViewModel> Create([FromBody] UserControllersViewModel obj)
        {
            //string Uploade = "/Content/Uploads/UserImage/";
            //IFormFile? s = obj.uploadedimg;
            //string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            //obj.Img = Uploade + NewFileName;
            //FileStream fs = new FileStream(Path.Combine(
            //    Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
            //    ), FileMode.Create);
            //s.CopyTo(fs);
            //fs.Position = 0;
            obj.Role = "User";
            obj.Img = "notprived.jpg";
            var result=
            await UserRep.Add(obj);
            if (result.Succeeded)
            {
                return new APIViewModel{
                    Success = true,
                    url = "User/Login",
                    Massege ="",
                    Data = null
                };
            }
            else return new APIViewModel
            {
                Massege = "Wrong Information",
                Success =false,
                url = "Register",
                Data = null
            };
        }
        [Route("User/UpdateProfile")]
        public APIViewModel Update(string ID)
        {
            var obj= UserRep.GetByID(ID);
            return new APIViewModel()
            {
                Success = true,
                Massege = "",
                Data=obj,
                url = "User/UpdateProfile"
            };
        }
        [Route("User/Profile")] 
        public async Task<APIViewModel> edit([FromBody] UserControllersViewModel obj)
        {
            //string Uploade = "/Content/Uploads/UserImage/";
            //IFormFile? s = obj.uploadedimg;
            //string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            //obj.Img = Uploade + NewFileName;
            //FileStream fs = new FileStream(Path.Combine(
            //    Directory.GetCurrentDirectory(), "Content", "Uploads", "UserImage", NewFileName
            //    ), FileMode.Create);

            //s.CopyTo(fs);
            //fs.Position = 0;
            await UserRep.Update(obj);
            unit.Submit();
            return new APIViewModel
            {
                Data = null,
                Success = true,
                Massege = "your Information updated Successfully",
                url = "User/Profile"
            };
        }

        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> SoftDelete(string ID)
        //{
        //    await UserRep.Delete(ID);
        //    unit.Submit();
        //    return RedirectToAction("AllUsers", "User");
        //}
        //public async Task<IActionResult> UpdateRole(string ID)
        //{
        //    await UserRep.AddRoleToUser(ID);
        //    return RedirectToAction("AllUsers", "User");
        //}
    }
}
