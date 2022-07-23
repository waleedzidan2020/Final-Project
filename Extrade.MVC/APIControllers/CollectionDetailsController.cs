using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using Extrade.ViewModels;
using extrade.models;

namespace Extrade.MVC.Controler
{
    public class CollectionDetailsController : Controller
    {


        CollectionDetailsRepository CollectionDetailsRepo;
        ProductRepository ProdRepo;
        private UnitOfWork UnitOfWork;
        public CollectionDetailsController(ProductRepository _prodrepo,CollectionDetailsRepository _CollectionDetailsRepo,
        UnitOfWork _UnitOfWork)
        {
            ProdRepo=_prodrepo;
            CollectionDetailsRepo = _CollectionDetailsRepo;
            UnitOfWork = _UnitOfWork;
        }
        public APIViewModel Get(int CollectionID)
        {

            var data =
            CollectionDetailsRepo.GetList().Where(p=>p.CollectionID== CollectionID).ToList();
            List<Product> Prod= new List<Product>();
            for (int i= 0; i < data.Count(); i++)
            {
                Prod.Add(ProdRepo.GetList().Where(p=>p.ID== data[i].ProductID).FirstOrDefault());
            }
                
                
               return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = Prod
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

