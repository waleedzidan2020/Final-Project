using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Extrade.MVC
{
    public class RoleController : Controller
    {
        private readonly RoleRepository roleRepository;
        private readonly UserRepository UserRepository;
        UnitOfWork UnitOfWork;
        public RoleController(ExtradeContext context,
            RoleRepository _roleRepository,
            UnitOfWork _UnitOfWork
            ,UserRepository _UserRepository)
        {
            UserRepository = _UserRepository;
            roleRepository = _roleRepository;
            UnitOfWork = _UnitOfWork;
        }
        [HttpGet]
        public IActionResult Add()
        {
          return View();
        }
        [HttpPost]
      
        public async Task<IActionResult> Add(RoleEditViewModel obj)
        {
            if (ModelState.IsValid) 
            { 
                await roleRepository.AddRole(obj);
                return RedirectToActionPermanentPreserveMethod("AllUsers","User", "Mvc/AllUsers");


            }
            else
            {
                return View();
            }
        }
        
        
        //public async Task<IActionResult> Update(UserControllersViewModel obj,string Role) { 
            
        //    await roleRepository.UpdateRole(obj,Role);
        //    UnitOfWork.Submit();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
