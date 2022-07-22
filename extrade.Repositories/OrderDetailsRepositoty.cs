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
        private readonly PaymentRepository PaymentRepo;
        public OrderDetailsRepositoty(ExtradeContext _DBContext,
            OrderRepository OrderRepository,
            VendorRepository vendorRepository,
            PaymentRepository _paymentRepo) : base(_DBContext)
        {
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
                vendor.Balance += (OrderDetails[i].SubPrice*(10/100));
                var VendorBalance = VendorRepository.Update(vendor.ToEditViewModel().ToModel()).Entity;
                if (GetOrder.CollectionCode != null) { 
                    Paymentacc.Balance += updatingTotalPrice.TotalPrice*(95/100);
                }
                else
                {
                    Paymentacc.Balance += updatingTotalPrice.TotalPrice / 100;
                }
                var payment = PaymentRepo.Add(Paymentacc).Entity;
            };
            return result;
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
