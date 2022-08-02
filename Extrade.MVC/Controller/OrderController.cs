using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using extrade.models;
using Extrade.ViewModels;
using System.Security.Claims;

namespace Extrade.MVC
{
    public class OrderController : Controller
    {
        private readonly OrderRepository repo;
        private readonly UnitOfWork UnitOfWork;
        private readonly OrderDetailsRepositoty repos;


        public OrderController(OrderRepository repo, UnitOfWork UnitOfWork, OrderDetailsRepositoty repos)
        {

            this.repo = repo;
            this.UnitOfWork = UnitOfWork;
            this.repos = repos;



        }
        [Route("Mvc/Order")]
        public IActionResult Get(int _id = 0, OrderStatus status = OrderStatus.pending, string NameUserEn = "", string NameUserAr = "", string DriverNameEn = "", string DriverNameAr = "", DateTime? time = null, float TotalPrice = 0, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var data = repo.GetList(_id, status, NameUserEn, NameUserAr, DriverNameEn, DriverNameAr, time, TotalPrice, orderby, IsAsceding, pageindex, pagesize);

            return View(data);
        }

        [HttpGet]
        public void GetOne(int id)
        {


            var res = repo.GetOne(id);

            repo.Remove(id);
            UnitOfWork.Submit();

        }


        public IActionResult EditOrderStatus(int id ) {

          var s =  repo.EditOrderStatus(id);
            var x = s.status;
            UnitOfWork.Submit();
        return    RedirectToAction("GetOrderDetails");
        }
      

         [HttpGet]

        [Route("Mvc/GetOrderDetails")]
        public IActionResult GetOrderDetails( )
        {
            ViewBag.userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = repos.GetListForOrderDetails();
        
            return View(data);


        }



        [HttpGet]

        [Route("Mvc/GetAllOrderDetails")]
        public IActionResult GetAllOrderDetails()
        {
            ViewBag.userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = repos.GetListForOrderDetails();

            return View(data);


        }
    }
}