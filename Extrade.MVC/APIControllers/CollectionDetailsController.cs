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
            List<ProductViewModel> Prod= new List<ProductViewModel>();
            for (int i= 0; i < data.Count(); i++)
            {
                Prod.Add(ProdRepo.GetList().Where(p=>p.ID== data[i].ProductID).FirstOrDefault().ToViewModel());
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
        public APIViewModel AddAPI([FromBody] List<CollectionDetalisEditViewModel> model)
        {
            try
            {

                foreach (var i in model)
                {
                    CollectionDetailsRepo.Add(i);
                }
                UnitOfWork.Submit();

                return new APIViewModel
                {
                    Data = null,
                    Success = true,
                    Massege = "Added"
                };
            }
            catch(Exception e)
            {
                return new APIViewModel
                {
                    Data = e.ToString(),
                    Success = false,
                    Massege = "error"
                };
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody]List<CollectionDetalisEditViewModel> model)
        {
            List<CollectionDetalisEditViewModel> added = new List<CollectionDetalisEditViewModel>();
            foreach(var i in model) { 
            CollectionDetailsRepo.Add(i);
                added.Add(i);
            }
            UnitOfWork.Submit();
            
            return RedirectToAction("GetProductWithCollection","Product", added);
        }
        [HttpGet]
        public APIViewModel GetByCode(string Code)
        {
            var reult = CollectionDetailsRepo.GetProductsByCode(Code);
            return new APIViewModel
            {
                Massege = "Done",
                Success = true,
                Data = reult,

            };
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

