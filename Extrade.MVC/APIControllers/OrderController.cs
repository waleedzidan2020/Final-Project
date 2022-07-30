using Microsoft.AspNetCore.Mvc;
using Extrade.Repositories;
using extrade.models;
using Extrade.ViewModels;
using System.Security.Claims;

namespace Extrade.MVC.Controler
{
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository repo;
        private readonly MarketerRebository MarketerRebository;
        private readonly CollectionRepository Collectionrepo;
        private readonly CollectionDetailsRepository Coldetailsrepo;
        private readonly CartRepository Cartrepo;
        private readonly OrderDetailsRepositoty detailsrepo;
        private readonly UserRepository UserRepo;
        private readonly ProductRepository ProdRepo;


        private readonly UnitOfWork UnitOfWork;


        public OrderController(ProductRepository _prorep,
            OrderRepository repo
            , CartRepository _Cartrepo
            , MarketerRebository _MarketerRebository
            , UnitOfWork UnitOfWork
            , CollectionRepository collectionrepo
            , UserRepository user
            , OrderDetailsRepositoty _ord
            , CollectionDetailsRepository collectionDetailsRepository)
        {
            ProdRepo= _prorep;
            UserRepo = user;
            this.repo = repo;
            detailsrepo = _ord;
            this.UnitOfWork = UnitOfWork;
            MarketerRebository= _MarketerRebository;
            Collectionrepo = collectionrepo;
            Cartrepo= _Cartrepo;
            Coldetailsrepo = collectionDetailsRepository;
        }

        public APIViewModel Get(int _id = 0, OrderStatus status = OrderStatus.pending, string NameUserEn = "", string NameUserAr = "", string DriverNameEn = "", string DriverNameAr = "", DateTime? time = null, float TotalPrice = 0, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {
            var data= repo.GetList(_id, status, NameUserEn, NameUserAr, DriverNameEn, DriverNameAr, time, TotalPrice, orderby, IsAsceding, pageindex, pagesize);

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = data.Data
            };
        }
        [HttpPost]
        public APIViewModel GetForUser(int ID)
        {
            
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = repo.GetOne(ID);
            var result = detailsrepo.GetList().Where(p => p.OrderId == order.ID);

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = result
            };
        }
        [HttpPost]
        public IActionResult Add(OrderEditViewModel order)
        {
            var LoginAccount = User.FindFirstValue(ClaimTypes.NameIdentifier);
            order.UserID = LoginAccount;
            var Insert = repo.Add(order);
            var odlist = new List<OrderDetailsEditViewModel>();
            var carts = Cartrepo.GetList().Where(i => i.UserID == LoginAccount).ToList();
            foreach(var i in carts)
            {
                var prod = ProdRepo.GetProductByID(i.ProductID);
                var oneod = new OrderDetailsEditViewModel()
                {
                    OrderID = Insert.ID,
                    ProductID = prod.ID,
                    ProductQuantity = i.Quantity,
                    SubPrice = prod.Price*i.Quantity,
                    
                };
                odlist.Add(oneod);
            }

            var orderdetails = detailsrepo.Add(odlist); 
            UnitOfWork.Submit();
            return RedirectToAction("Add", "OrderDetails", Insert);
            
            
        }

        public IActionResult AddFromCollection(OrderEditViewModel order)
        {
            
            var colID = Collectionrepo.GetOneByCode(order.CollectionCode);
                Coldetailsrepo.GetList().Where(p=>p.CollectionID == colID.ID).ToList();
                var query = Collectionrepo.GetList().Where(p => p.Code == order.CollectionCode)
                    .FirstOrDefault();
                var coldetails = Coldetailsrepo.GetList().Where(c => c.CollectionID == query.ID).ToList();
                var Insertsalary = repo.Add(order);
                UnitOfWork.Submit();
                var orderdetails = detailsrepo.AddForCollection(coldetails,Insertsalary);
            
                    var marketer = MarketerRebository.GetOne(query.MarketerID);
                    marketer.Salary += Insertsalary.TotalPrice * (5 / 100);
                    MarketerRebository.UpdateSalary(marketer);
                    UnitOfWork.Submit();
                    return RedirectToAction("Add", "OrderDetails", Insertsalary);
        }       
        //[HttpGet]
        //public APIViewModel Update(int ID)
        //{
        //    var result= repo.GetOne(ID);
        //    return new APIViewModel
        //    {
        //        Success = true,
        //        Massege = "Your order is on its way to you",
        //        Data = result
        //    };
        //}
        //[HttpPost]
        //public APIViewModel Update([FromBody] OrderEditViewModel obj)
        //{
        //    var result = repo.Update(obj);
        //    UnitOfWork.Submit();
        //    return new APIViewModel
        //    {
        //        Success = true,
        //        Massege = "Update Successfully",
        //        Data = result
        //    };
        //}

        public APIViewModel Remove(int ID)
        {
            repo.Remove(ID);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Order has Cancelled",
                Data = null
            };
        }
        [HttpPost]
        public APIViewModel Delete(int id) {
           var res = repo.GetOne(id);
            repo.Delete(res);
            UnitOfWork.Submit();
            return new APIViewModel
            {
                Success = true,
                Massege = "Delete Succeeded",
                Data = null
            };
        }
    }
}
