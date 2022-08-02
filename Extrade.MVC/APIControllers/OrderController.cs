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
            var result = detailsrepo.GetList().Where(p => p.OrderID == order.ID);

            return new APIViewModel
            {
                Success = true,
                Massege = "",
                Data = result
            };
        }
        [HttpPost]
        public APIViewModel AddApi([FromBody] OrderEditViewModel order)
        {
            try
            {
                order.DriverID = 0;
                var Insert = repo.Add(order);
                UnitOfWork.Submit();
                var lastOrder = repo.GetlastOrder(order.UserID,OrderStatus.pending, 0, "ID", false);
                var odlist = new List<OrderDetailsEditViewModel>();
                var carts = Cartrepo.GetList().Where(i => i.UserID == order.UserID).ToList();
                foreach (var i in carts)
                {
                    var prod = ProdRepo.GetProductByID(i.ProductID);
                    var oneod = new OrderDetailsEditViewModel()
                    {
                        OrderID = lastOrder.ID,
                        ProductID = prod.ID,
                        ProductQuantity = i.Quantity,
                        SubPrice = prod.Price * i.Quantity,
                    };
                    odlist.Add(oneod);
                    Cartrepo.Remove(i);
                }

                var orderdetails = detailsrepo.Add(odlist);
                UnitOfWork.Submit();

                return new APIViewModel
                {
                    Massege = "added",
                    Success = true,
                    Data = Insert.ID,
                };
            }catch(Exception e)
            {
                return new APIViewModel
                {
                    Massege = "Error",
                    Success = false,
                    Data = null,
                };
            }

        }
        [HttpPost]
        public IActionResult Add(OrderEditViewModel order)
        {
            //var LoginAccount = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //order.UserID = LoginAccount;
            Random rand = new Random();
            order.DriverID = rand.Next(1,3);
            var Insert = repo.Add(order);
            UnitOfWork.Submit();
            var odlist = new List<OrderDetailsEditViewModel>();
            var carts = Cartrepo.GetList().Where(i => i.UserID == order.UserID).ToList();
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
        [HttpPost]
        public APIViewModel AddFromCollection([FromBody]OrderEditViewModel order)
        {
            try
            {
                order.DriverID = null;
                var colID = Collectionrepo.GetOneByCode(order.CollectionCode);
               
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
                return new APIViewModel
                {
                    Data = null,
                    Success = true
                };
            }catch(Exception e)
            {
                return new APIViewModel
                {
                    Data = e,
                    Success = false,
                    Massege = e.Message
                };
            }
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
