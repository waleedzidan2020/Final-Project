using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using LinqKit;

using Extrade.ViewModels;
using static Extrade.ViewModels.OrderDetailsExtension;

namespace Extrade.Repositories
{
    public class OrderDetailsRepositoty : GeneralRepositories<OrderDetails>
    {
        private readonly OrderRepository orderRepository;
        private readonly VendorRepository VendorRepository;
        private readonly MarketerRebository marketerRepository;
        private readonly PaymentRepository PaymentRepo;
        private readonly ProductRepository ProdRepo;
        public OrderDetailsRepositoty(ProductRepository _prodRepo,
            ExtradeContext _DBContext,
            OrderRepository OrderRepository,
            VendorRepository vendorRepository,
             MarketerRebository marketerRepository,
            PaymentRepository _paymentRepo) : base(_DBContext)
        {
            this.marketerRepository = marketerRepository;
            ProdRepo = _prodRepo;
            orderRepository = OrderRepository;
            VendorRepository = vendorRepository;
            PaymentRepo = _paymentRepo;
        }

        public List<OrderDetailsViewModel> Get(int OrderID) { 
           
        var query = base.GetList().Where(p=>p.OrderId==OrderID);
        var res = query.Select(p => new OrderDetailsViewModel()
            {
                SubPrice=p.SubPrice,
                ProductQuantity=p.Product.Quantity,
                TotalPrice=p.Order.TotalPrice,
            }).Where(p=>p.OrderID == OrderID);
            return res.ToList();
        }




        public List<OrderDetailsViewModel> GetList()
        {


            var query = base.GetList();
            var res = query.Select(p => new OrderDetailsViewModel()
            {
                SubPrice = p.SubPrice,
                ProductQuantity = p.Product.Quantity,
                OrderID = p.OrderId,
                TotalPrice = p.Order.TotalPrice,
                
            });
            return res.ToList();
        }



        public List<OrderDetailsViewModel> GetListForOrderDetails()
        {
            

            var query = base.GetList();
            var res = query.Select(p => new OrderDetailsViewModel()
            {
                SubPrice = p.SubPrice,
                ProductQuantity = p.Product.Quantity,
                OrderID = p.OrderId,
                ProductID = p.ProductId,
                productNameAr = p.Product.NameAr,
                productNameEn = p.Product.NameEn,
                TotalPrice = p.Order.TotalPrice,
                VendorId = p.Product.VendorID,
                PhoneNumber = p.Order.User.PhoneNumber,
                NameEn=p.Order.User.NameEn,
                OrderStatus=p.Order.OrderStatus
                 





            }) ;
            return res.ToList();
        }

        public List<OrderDetails> Add(List<OrderDetailsEditViewModel> OrderDetails)
        {
            var Paymentacc = new AllPayment();
            var result =new List<OrderDetails>();
            for(int i = 0; i < OrderDetails.Count; i++)
            {
                var GetOrder = orderRepository.GetOrderByID(OrderDetails[i].OrderID);
                base.Add(OrderDetails[i].ToModel()).Entity.ToViewModel();

                result.Add(OrderDetails[i].ToModel());
                var VendorID =VendorRepository.GetVendorbyProductID(OrderDetails[i].ProductID);
                var vendor =VendorRepository.GetOne(VendorID);
                GetOrder.TotalPrice += OrderDetails[i].SubPrice;
                var updatingTotalPrice = orderRepository.Update(GetOrder).Entity;
                vendor.Balance += OrderDetails[i].SubPrice - ((10/100)*OrderDetails[i].SubPrice);
                var VendorBalance = VendorRepository.Update(vendor.ToEditViewModel().ToModel()).Entity;
                Paymentacc.Balance += OrderDetails[i].SubPrice - ((90 / 100) * OrderDetails[i].SubPrice);
                
                var payment = PaymentRepo.Add(Paymentacc).Entity;
            };
            return result;
        }
        public List<OrderDetails> AddForCollection(List<CollectionDetails> CollectionDetails
            ,OrderViewModel order)
        {
            try
            {

                var Paymentacc = new AllPayment();
                var result = new List<OrderDetails>();
                var OrderModel = orderRepository.GetOrderByID(order.ID);
                for (var i = 0; i < CollectionDetails.Count; i++)
                {
                    var query = ProdRepo.GetProductModelByID(CollectionDetails[i].ProductID);
                    var od = new OrderDetails()
                    {
                        ProductId = query.ID,
                        Quantity = 1,
                        SubPrice = 1 * query.Price,
                        OrderId = OrderModel.ID
                    };
                    base.Add(od).Entity.ToViewModel();
                    result.Add(od);

                    var VendorID = VendorRepository.GetVendorbyProductID(query.ID);
                    var vendor = VendorRepository.GetOne(VendorID);
                    var marketer = marketerRepository.GetOneByCollection(CollectionDetails[i].CollectionID);
                    order.TotalPrice += od.SubPrice;
                    var updatingTotalPrice = orderRepository.Update(OrderModel).Entity;

                    //vendor.Balance += (od.SubPrice * (10 / 100));
                    vendor.Balance += od.SubPrice - ((10 / 100) * od.SubPrice);
                    var VendorBalance = VendorRepository.Update(vendor.ToEditViewModel().ToModel()).Entity;

                    Paymentacc.Balance += od.SubPrice - ((95 / 100) * od.SubPrice);
                    var payment = PaymentRepo.Add(Paymentacc).Entity;

                    marketer.Salary += od.SubPrice - ((95 / 100) * od.SubPrice);
                    var markterBalance = marketerRepository.Update(marketer.ToEditViewModel().ToModel()).Entity;
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

            //for (int i = 0; i < CollectionDetails.Count; i++)
            //{
            //    var GetOrder = orderRepository.GetOrderByID(OrderDetails[i].OrderId);

            //    if (GetOrder.CollectionCode != null)
            //    {

            //    }
            //    else
            //    {
            //        Paymentacc.Balance += updatingTotalPrice.TotalPrice / 100;
            //    }

            //};

        }
        public OrderDetails GetOneByID(int ID)
        {
            var filter = PredicateBuilder.New<OrderDetails>();
            filter = filter.Or(p=>p.ID==ID);
            var result = base.GetbyID(filter);
            return result;
        }
        public Order Remove(int ID)
        {
            var query = GetOneByID(ID);
            var GetOrder = orderRepository.GetOrderByID(query.OrderId);
            var del= base.Remove(query).Entity;
            GetOrder.TotalPrice += query.SubPrice;
            var updateTotalPrice = orderRepository.Update(GetOrder).Entity;
            return updateTotalPrice;
        }

    }
}
