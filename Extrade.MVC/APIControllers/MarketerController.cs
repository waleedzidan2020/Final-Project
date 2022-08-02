using Extrade.Repositories;
using Microsoft.AspNetCore.Mvc;
using extrade.models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Extrade.ViewModels;

namespace Extrade.MVC.Controler
{
    public class MarketerController : Controller
    {
        public readonly MarketerRebository marketerRebository;
        private readonly UnitOfWork unitOfWork;
        UserRepository userRepository;
        public MarketerController(MarketerRebository marketerRebository , UnitOfWork unitOfWork ,UserRepository userRepository)
        {


            this.marketerRebository = marketerRebository;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }
        public APIViewModel GetDetails(string _id = null)
        {
            var data = marketerRebository.GetOne(_id);
            return new APIViewModel
            {
                Success=true,
                Massege= "Done",
                Data=data
            };
        }
        [Authorize(Roles ="admin")]
        public APIViewModel Get( string _UserID = null, string _TaxCard = "", float _Salary = 0,
            string _CollectionNameEn = "", string _CollectionNameAr = "", bool IsDeleted = false,
                string orderby = "ID", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {
                var res = marketerRebository.Get( _UserID, _TaxCard, _Salary, _CollectionNameEn, _CollectionNameAr,
                  IsDeleted,orderby, isAscending, pageIndex, pageSize);

            return new APIViewModel
            {
                Success = true,
                Massege = "Done",
                Data = res
            }; 
        }

        
        [HttpGet]
        public IActionResult Add(User modeluser)
        {
            ViewBag.id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = marketerRebository.GetOne(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (data == null)
                return View();
            else
                return RedirectToAction("Get", "Collection");

        }

        //[HttpPost]
        //[Authorize(Roles = "Marketer")]
        //public IActionResult Add(MarketerEditViewModel model)
        //{
        //    string Uploade = "/Content/Uploads/Category/";

        //    IFormFile? s = model.ImageFile;


        //    string NewFileName = Guid.NewGuid().ToString() + s.FileName;
        //    model.TaxCard = Uploade + NewFileName;
        //    FileStream fs = new FileStream(Path.Combine(
        //        Directory.GetCurrentDirectory(), "Content", "Uploads", "Category", NewFileName
        //        ), FileMode.Create);
        //    s.CopyTo(fs);
        //    fs.Position = 0;
        //    marketerRebository.Add(model.ToModel());
        //    unitOfWork.Submit();
        //    return RedirectToAction("Get", "Collection");

        //}

        [HttpGet]
        public APIViewModel Update(string id)
        {
            try
            {

                var res = marketerRebository.GetOne(id);
                return new APIViewModel
                {
                    Data = res,
                    Success = true,
          
                };
            }
            catch(Exception e)
            {
                return new APIViewModel
                {
                    Data = null,
                    Success = false,

                };
            }
        }

        [HttpPost]

        public IActionResult Update(MarketerEditViewModel model)
        {
            marketerRebository.Update(model);

            unitOfWork.Submit();
            return RedirectToAction("Get", "Collection");
        }

        [HttpGet]
        public IActionResult Remove(MarketerEditViewModel model, int id)
        {

            var res = marketerRebository.Remove(model);


            unitOfWork.Submit();
            return RedirectToAction("Get");

        }
        public IActionResult Details(string _UserID = null, string _TaxCard = "", float _Salary = 0,
            string _CollectionNameEn = "", string _CollectionNameAr = "", bool IsDeleted = false,
                string orderby = "ID", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {
            var res =marketerRebository.Get(_UserID, _TaxCard, _Salary, _CollectionNameEn, _CollectionNameAr,
                IsDeleted, orderby, isAscending, pageIndex, pageSize);
            return View(res.Data);
        }

        public IActionResult AcceptMarketer(string ID)
        {
            marketerRebository.AcceptMarketer(ID);
            unitOfWork.Submit();
            return View("Get");
        }

        public IActionResult RejectMarketer(string ID)
        {
            marketerRebository.RejectMarketer(ID);
            unitOfWork.Submit();
            return View("Get");
        }



    }
}
