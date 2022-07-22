using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using extrade.models;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Extrade.MVC
{
    public class OrderController : Controller
    {
        private readonly OrderRepository repo;
        private readonly UnitOfWork UnitOfWork;


        public OrderController(OrderRepository repo, UnitOfWork UnitOfWork)
        {

            this.repo = repo;
            this.UnitOfWork = UnitOfWork;



        }
        [Authorize("Admin")]
        [Route("Mvc/Order")]
        public IActionResult Get(int _id = 0, OrderStatus status = OrderStatus.pending, string NameUserEn = "", string NameUserAr = "", string DriverNameEn = "", string DriverNameAr = "", DateTime? time = null, float TotalPrice = 0, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var data = repo.GetList(_id, status, NameUserEn, NameUserAr, DriverNameEn, DriverNameAr, time, TotalPrice, orderby, IsAsceding, pageindex, pagesize);

            return View(data);
        }

        [Authorize("User")]
        [Route("Api/Order")]
        public APIViewModel GetApi(int _id = 0, OrderStatus status = OrderStatus.pending, string NameUserEn = "", string NameUserAr = "", string DriverNameEn = "", string DriverNameAr = "", DateTime? time = null, float TotalPrice = 0, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var data = repo.GetList(_id, status, NameUserEn, NameUserAr, DriverNameEn, DriverNameAr, time, TotalPrice, orderby, IsAsceding, pageindex, pagesize);
            return new APIViewModel
            {
                Massege="",
                Data=data,
                Success=true,
                url=""
                
                


            };
            
        }

    }
}