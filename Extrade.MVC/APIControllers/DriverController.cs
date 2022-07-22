using extrade.models;
using Extrade.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Extrade.MVC.APIControllers
{
    public class DriverController : Controller
    {
        private readonly DriverRepository DriverRepo;
        private readonly UnitOfWork unitOfWork;
        public DriverController(UnitOfWork _unitOfWork,DriverRepository _DriverRepo)
        {
            unitOfWork = _unitOfWork;
            DriverRepo = _DriverRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Driver obj)
        {
            var result = DriverRepo.Add(obj);
            unitOfWork.Submit();
            return View("DriverProfile",result);
        }
        [HttpPost]
        public IActionResult Update(int ID)
        {
            var query = DriverRepo.GetDriverByID(ID);
            var result = DriverRepo.Update(query);
            unitOfWork.Submit();
            return View("DriverProfile",result);
        }
    }
}
