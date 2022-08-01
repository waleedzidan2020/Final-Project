
using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using extrade.models;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static Extrade.ViewModels.OrderDetailsExtension;

namespace Extrade.MVC.Controler

{
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsRepositoty repo;
        private readonly UnitOfWork unitOfWork;
        private readonly OrderController OrderController;
        private readonly UserRepository userrepo;
        private readonly CartRepository Cartrepo;
        private readonly ProductRepository Productrepo;
        public OrderDetailsController(OrderDetailsRepositoty _repo
            , OrderController _OrderController, UnitOfWork unitOfWork,
            UserRepository _userrepo, CartRepository _Cartrepo, ProductRepository _Productrepo)
        {
            Cartrepo = _Cartrepo;
            Productrepo = _Productrepo;
            OrderController = _OrderController;
            repo = _repo;
            this.unitOfWork = unitOfWork;
            this.userrepo = _userrepo;
        }

        public APIViewModel Get(int OrderID)
        {
            var data = repo.Get(OrderID);

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data
            };
        }



        //[HttpPost]
        //public APIViewModel Add(OrderViewModel order)
        //{
        //public IActionResult GetList()
        //{
        //    var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var data = repo.GetList().Where(p=>p.product.VendorID==userid);


        //}

        //[HttpPost]
        //public APIViewModel Add(OrderViewModel order)
        //{

        //    //    var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //    var user = userrepo.GetUserByID(userID);
        //    //    var carts = Cartrepo.GetList().Where(i => i.UserID == userID).ToList();
        //    //    var pro=carts.Select(p=>p.Product.Quantity).ToList();
        //    //    for (int x = 0; x < pro.Count; x++) { 
        //    //        if (pro[x] > 0) { 
        //    //    List<OrderDetailsEditViewModel>? orderDetail = new List<OrderDetailsEditViewModel>();
        //    //    for(int i = 0; i < carts.Count; i++) {
        //    //        orderDetail.Add( new OrderDetailsEditViewModel
        //    //    {
        //    //        OrderID=order.ID,
        //    //        ProductID = carts[i].ProductID,
        //    //        ProductQuantity=carts[i].Quantity,
        //    //        SubPrice = carts[i].Product.Price * carts[i].Quantity,
        //    //    });
        //    //    };
        //    //    var result = repo.Add(orderDetail);

        //    //    unitOfWork.Submit();
        //    //    return new APIViewModel
        //    //    {
        //    //        Success = true,
        //    //        Massege = "Done",
        //    //        Data = result
        //    //    };
        //    //    }
        //    //    };
        //    //    return new APIViewModel
        //    //    {
        //    //        Massege = "Sorry Quantity not enough",
        //    //        Data = null,
        //    //        Success = false,

        //    //    };
        //    //}



        }
    }

