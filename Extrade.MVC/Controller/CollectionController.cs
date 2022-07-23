using extrade.models;
using Extrade.Repositories;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Mvc;



namespace Extrade.MVC
{
    public class CollectionController : Controller
    {


        private CollectionRepository CollectionRepo;
        private UnitOfWork UnitOfWork;
        public CollectionController(CollectionRepository _CollectionRepo,
         UnitOfWork _UnitOfWork)
        {

            CollectionRepo = _CollectionRepo;
            UnitOfWork = _UnitOfWork;
        }
        //[Route("Mvc/Collection")]
        //public ViewResult Get(string id,string NameAr = "", string? NameEN = "",
        //    string Namepenroduct = "", string Namearproduct = "",
        //    string Description = "", float Price = 0, int Quantity = 0, ProductStatus? Status = null,
        //        string OrderBy = "",
        //   bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        //{
        //    var data =
        //   CollectionRepo.Get( id,NameAr, NameEN,
        //   Namepenroduct, Namearproduct, Description, Price, Quantity, Status, OrderBy,
        //       IsAsceding, pageindex, pagesize);
        //    return View(data.Data);
        //}

        //public IActionResult Search(int pageindex = 1, int pagesize = 20)
        //{

        //    var Data = CollectionRepo.Search(pageindex, pagesize);
        //    return View("Get", Data);
        //}


        
        //[HttpPost]
        //public IActionResult Add(CollectionEditViewModel model)
        //{
        //    CollectionRepo.Add(model);
        //    UnitOfWork.Submit();
        //    return RedirectToAction("Search");
        //}


        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            var ids =
                    CollectionRepo.GetOne(id);

            return RedirectToAction("Add");

        }
        [HttpPost]

        public IActionResult Update(CollectionEditViewModel model)
        {
            return View();
        }






        //public IActionResult Remove()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Remove(CollectionEditViewModel model)
        //{
        //    return 0;
        //}


    }
}
