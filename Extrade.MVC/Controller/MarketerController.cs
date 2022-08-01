using Extrade.Repositories;
using Microsoft.AspNetCore.Mvc;
using extrade.models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Extrade.ViewModels;

namespace Extrade.MVC
{
    public class MarketerController : Controller
    {
        public readonly MarketerRebository marketerRebository;
        private readonly UnitOfWork unitOfWork;
        UserRepository userRepository;
        RoleRepository role;
        public MarketerController(MarketerRebository marketerRebository, UnitOfWork unitOfWork, UserRepository userRepository, RoleRepository role)
        {

            this.role = role;
            this.marketerRebository = marketerRebository;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;

        }
        public ViewResult GetOne(string _id = null)
        {
            var data = marketerRebository.GetOne(_id);
            return View(data);
        }
       [Authorize(Roles ="Admin")]
       [Route("MarketerMVC/Get")]
        public IActionResult Get(string _UserID = null, string _TaxCard = "", float _Salary = 0,
            string _CollectionNameEn = "", string _CollectionNameAr = "", bool IsDeleted = false,
                string orderby = "", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {
            var res = marketerRebository.Get(_UserID, _TaxCard, _Salary, _CollectionNameEn, _CollectionNameAr,
              IsDeleted, orderby, isAscending, pageIndex, pageSize);


            return View(res.Data);
        }

        //[Authorize(Roles = "Marketer")]
        //[HttpGet]
        //public IActionResult Add(User modeluser)
        //{
        //    ViewBag.id = User.FindFirstValue(ClaimTypes.NameIdentifier);


        //    var data = marketerRebository.GetOne(User.FindFirstValue(ClaimTypes.NameIdentifier));



        //    if (data == null)
        //        return View();
        //    else
        //        return RedirectToAction("Get", "Collection");

        //}

        [HttpPost]
        //[Authorize(Roles = "Marketer")]
        [Route("Api/AddMarketer")]
        public async Task<ObjectResult> Add([FromBody]UserMarketerEditViewModel model)
        {
            //string Uploade = "/Content/Uploads/Category/";

            //IFormFile s = model.ImageFile;


            //string NewFileName = Guid.NewGuid().ToString() + s.FileName;
            //model.TaxCard = Uploade + NewFileName;


            //FileStream fs = new FileStream(Path.Combine(
            //    Directory.GetCurrentDirectory(), "Content", "Uploads", "Category", NewFileName


            //    ), FileMode.Create);
            // model.TaxCard = "  ";

            //s.CopyTo(fs);
            //fs.Position = 0;try{
            try
            {
                model.TaxCard = "  ";
               
                model.Role = "Marketer";
                var res = await marketerRebository.Add(model);
                unitOfWork.Submit();


                return new ObjectResult(new
                {

                    Success = true

                });
            }
            catch(Exception e)
            {
                return new ObjectResult(new
                {

                    Success = false

                });
            }
        }

        //[HttpGet]

        //public ViewResult Update(string id)
        //{

        //    var res = marketerRebository.GetOne(id);

        //    return View(res.ToEditViewModel());


        //}

        //[HttpPost]

        //public IActionResult Update(MarketerEditViewModel model)
        //{
        //    marketerRebository.Update(model);

        //    unitOfWork.Submit();
        //    return RedirectToAction("Get", "Collection");
        //}

        //[HttpGet]
        //public IActionResult Remove(MarketerEditViewModel model, int id)
        //{

        //    var res = marketerRebository.Remove(model);


        //    unitOfWork.Submit();
        //    return RedirectToAction("Get");

        //}
        [Route("Mvc/AllMarketers")]
        public IActionResult Details(string _UserID = null, string _TaxCard = "", float _Salary = 0,
            string _CollectionNameEn = "", string _CollectionNameAr = "", bool IsDeleted = false,
                string orderby = "UserID", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {


            var res = marketerRebository.Get(_UserID, _TaxCard, _Salary, _CollectionNameEn, _CollectionNameAr,
                IsDeleted, orderby, isAscending, pageIndex, pageSize);



            return View(res.Data);
        }



        
        public async Task<IActionResult> SoftDelete(string ID)
        {
            await userRepository.Delete(ID);
            await marketerRebository.Delete(ID);
            unitOfWork.Submit();
            return RedirectToAction("Get");
        }

        public IActionResult AcceptMarketer(string ID)
        {
            marketerRebository.AcceptMarketer(ID);
            unitOfWork.Submit();
            return View("Get");
        }
        public IActionResult RejectProduct(string ID)
        {
            marketerRebository.RejectMarketer(ID);
            unitOfWork.Submit();
            return View("Get");
        }



    }
}