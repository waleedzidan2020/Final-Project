using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using Extrade.ViewModels;

namespace Extrade.MVC.Controler
{
    public class CollectionDetailsController : Controller
    {


        CollectionDetailsRepository CollectionDetailsRepo;
        private UnitOfWork UnitOfWork;
        public CollectionDetailsController(CollectionDetailsRepository _CollectionDetailsRepo,

        UnitOfWork _UnitOfWork)
        {
            
            CollectionDetailsRepo = _CollectionDetailsRepo;
            UnitOfWork = _UnitOfWork;
        }
        public APIViewModel Get(
            int CollectionID)
        {

            var data =
           CollectionDetailsRepo.GetList().Where(p=>p.CollectionID== CollectionID);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data
            };
            //return View(data.Data);
        }
        public APIViewModel Search(int pageIndex = 1, int pageSize = 20)
        {
            var Data = CollectionDetailsRepo.Search(pageIndex, pageSize);
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = Data
            };
            //return View("Get", Data);
        }
        
        [HttpPost]
        public IActionResult Add(CollectionDetalisEditViewModel model)
        {
            CollectionDetailsRepo.Add(model);
            UnitOfWork.Submit();
            return RedirectToAction("GetProductWithCollection","Product", model.CollectionID);
        }
        [HttpPost]
        public APIViewModel Delete(CollectionDetalisEditViewModel obj)
        {
            CollectionDetailsRepo.Delete(obj);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = null
            };
        }

    }
}

