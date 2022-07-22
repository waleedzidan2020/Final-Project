﻿
//using extrade.models;
//using Microsoft.AspNetCore.Mvc;
//using Extrade.Repositories;
//using Extrade.ViewModels;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;


//namespace Extrade.MVC.Controler
//{
//    public class VendorController : Controller
//    {
//        private readonly VendorRepository repo;
//        private readonly UnitOfWork unitOfWork;
//        UserRepository userRepository;

//        public VendorController(VendorRepository repo, UnitOfWork unitOfWork, UserRepository userRepository)
//          { 
//            this.repo = repo;
//            this.unitOfWork = unitOfWork;
//            this.userRepository = userRepository;



//        }
//        public APIViewModel GetOne(string? _id = null) 
//        {
//            var  data =   repo.GetOne(_id);
//            return new APIViewModel
//            {
//                Data = data,
//                Massege = "",
//                Success=true
//            };
//        }


//        [Authorize(Roles = "Admin")]
//        public ViewResult GetList(string _id = null, string _BrandNameAr = "", string _BrandNameEn = "", string _NameProductAr = "", string _NameProductEn = "",bool IsDeleted=false, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
       
//        {

//            var res = repo.GetList(_id, _BrandNameAr, _BrandNameEn, _NameProductAr, _NameProductEn,IsDeleted, orderby , IsAsceding, pageindex, pagesize);



//            return View(res.Data);
//        }
//        [Authorize(Roles = "Vendor")]
//        [HttpGet]
//        public IActionResult Add(User modeluser) {
//            ViewBag.id =  User.FindFirstValue(ClaimTypes.NameIdentifier);
//          var data = repo.GetOne(User.FindFirstValue(ClaimTypes.NameIdentifier));
//            if (data==null  ) 
//                return View();
//            else
//                return RedirectToAction("Get", "Category");
        
//        }
        
//        [HttpPost]
//        [Authorize(Roles = "Vendor")]
//        public IActionResult Add(VendorEditViewModel  model)
//        {
//            if (model.VendorStatus == VendorStatus.accepted) { 
//            string Uploade = "/Content/Uploads/Vendor/";
//            model.VendorImage = new List<string>();
//            foreach (IFormFile f in model.ImageFile)
//            {
//                string NewFileName = Guid.NewGuid().ToString() + f.FileName;
//                model.VendorImage.Add(Uploade + NewFileName);
//                FileStream fs = new FileStream(Path.Combine(
//               Directory.GetCurrentDirectory(), "Content", "Uploads", "Vendor",NewFileName
//               ), FileMode.Create);
//                f.CopyTo(fs);
//                fs.Position = 0;
//            }
//                repo.Add(model.ToModel());
//                unitOfWork.Submit();
//                return RedirectToAction("VendorProducts", "Vendor");
//            }
//            else return View("SorryVendor");
//        }

//        [HttpGet]
//        [Authorize(Roles = "Vendor")]
//        public ViewResult Update(string id)
//        {
//            var res = repo.GetOne(id);
//            return View(res.ToEditViewModel());
//        }
      





//        [HttpPost]
      
//        public IActionResult Update(VendorEditViewModel model)
//        {

//            string Uploade = "/Content/Uploads/Vendor/";
//            model.VendorImage = new List<string>();

//            foreach (IFormFile f in model.ImageFile)
//            {
//                string NewFileName = Guid.NewGuid().ToString() + f.FileName;
//                model.VendorImage.Add(Uploade + NewFileName);
//                FileStream fs = new FileStream(Path.Combine(
//               Directory.GetCurrentDirectory(), "Content", "Uploads", "Vendor", NewFileName


//               ), FileMode.Create);

//                f.CopyTo(fs);
//                fs.Position = 0;

//            }



//            repo.Update(model);

//            unitOfWork.Submit();
//            return RedirectToAction("Get", "Category");
//        }


//        [HttpGet]
//        public IActionResult Remove(VendorEditViewModel model, int id)
//        {

//            var res = repo.Remove(model);


//            unitOfWork.Submit();
//            return RedirectToAction("GetList");




//        }

//        public IActionResult Details(string _id = "", string _BrandNameAr = "", string _BrandNameEn = "",bool IsDeleted=false, string _NameProductAr = "", string _NameProductEn = "", string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
//        {


//            var res = repo.GetList(_id, _BrandNameAr, _BrandNameEn, _NameProductAr, _NameProductEn,  IsDeleted, orderby, IsAsceding, pageindex, pagesize);



//            return View(res.Data);


            


//        }
//        public IActionResult AcceptVendor(string ID)
//        {
//            repo.AcceptVendor(ID);
//            unitOfWork.Submit();
//            return null;
//        }
//        public IActionResult RejectVendor(string ID)
//        {
            
//            repo.RejectVendor(ID);
//            unitOfWork.Submit();
//            return null;
//        }

//    }


   
//}
