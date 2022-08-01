using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using LinqKit;
using Extrade.ViewModels;

using Extrade.Repositories;

namespace Extrade.Repositories
{
    public class OrderRepository :GeneralRepositories<Order>
    {

        public OrderRepository(ExtradeContext _DBContext) : base(_DBContext) { }
        public PaginingViewModel<List<OrderViewModel>> GetList(int _id = 0, OrderStatus status = OrderStatus.pending, string NameUserEn = "", string NameUserAr = "", string DriverNameEn = "", string DriverNameAr = "",DateTime? time=null,float TotalPrice=0, string orderby = null, bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {

            var filterd = PredicateBuilder.New<Order>();
            var old = filterd;
            if (_id > 0)
                filterd = filterd.Or(i => i.ID == _id);
            if (status != OrderStatus.pending)
                filterd = filterd.Or(i => i.OrderStatus == status);
            if (!string.IsNullOrEmpty(NameUserEn))
                filterd = filterd.Or(i => i.User.UserName.Contains(NameUserEn) );
            if (!string.IsNullOrEmpty(NameUserAr))
                filterd = filterd.Or(i => i.User.NameAr.Contains(NameUserAr));
            if (!string.IsNullOrEmpty(DriverNameEn))
                filterd = filterd.Or(i => i.Driver.NameEn .Contains( DriverNameEn));
            if (!string.IsNullOrEmpty(DriverNameAr))
                filterd = filterd.Or(i => i.Driver.NameAr.Contains(DriverNameAr));
            if (time != null)
                filterd = filterd.Or(p => p.ArrivalDate.Value == time.Value);
            if (TotalPrice > 0)
                filterd = filterd.Or(p => p.TotalPrice == TotalPrice);

            if (old == filterd)
                filterd = null;

            var query = base.Get(filterd, orderby, IsAsceding, pageindex, pagesize, null);
            var res = query.Select(p => new OrderViewModel()
            {
                ID = p.ID,
                OrderDetails = p.OrderDetails.Select(p => p.Quantity).ToList(),
                TotalPrice =p.TotalPrice,
                status = p.OrderStatus,
            });

            PaginingViewModel<List<OrderViewModel>> final = new PaginingViewModel<List<OrderViewModel>>()
            {
                Count = base.GetList().Count(),
                Data = res.ToList(),
                PageIndex = pageindex,
                PageSize = pagesize


            };
            return final;

        }
        public OrderViewModel GetlastOrder(string UserID = "",OrderStatus status = OrderStatus.pending, float TotalPrice = 0, string orderby = "ID", bool IsAsceding = false)
        {

            var filterd = PredicateBuilder.New<Order>();
            var old = filterd;

            if (!string.IsNullOrEmpty(UserID))
                filterd = filterd.Or(i => i.UserID == UserID);
            if (status != OrderStatus.pending)
                filterd = filterd.Or(i => i.OrderStatus == status);

            if (TotalPrice > 0)
                filterd = filterd.Or(p => p.TotalPrice == TotalPrice);

            if (old == filterd)
                filterd = null;

            var query = base.Get(filterd, orderby, IsAsceding, 1, 20, null);
            var res = query.Select(p => new OrderViewModel()
            {
                ID = p.ID,
                OrderDetails = p.OrderDetails.Select(p => p.Quantity).ToList(),
                TotalPrice = p.TotalPrice,
                status = p.OrderStatus,
            }).FirstOrDefault();

            return res;

        }

        public Order GetOrderByID(int ID)
        {
            var filterd = PredicateBuilder.New<Order>();
            if (ID > 0)
                filterd.Or(p => p.ID == ID);
            var res = GetByID(filterd);
            return res;
        }

        public OrderViewModel GetOne(int id) {

            var filterd = PredicateBuilder.New<Order>();
            if (id > 0)
                filterd.Or(p => p.ID == id);

          var res =  GetByID(filterd);
            return res.ToViewModel();


        }
        public OrderViewModel Add(OrderEditViewModel obj)=>
           base.Add(obj.ToModel()).Entity.ToViewModel();

        //public OrderViewModel Update(OrderEditViewModel obj)
        //{
        //    var filter = PredicateBuilder.New<Order>();
        //    filter = filter.Or(c => c.ID == obj.ID);
        //    var query = base.GetByID(filter);
        //    query.TotalPrice = obj.TotalPrice;
            
        //    query.ShippingDate = DateTime.Now;
            
        //    return base.Update(query).Entity.ToViewModel();
        //}
        public List<int> CheckingList(List<CollectionDetails> collections,List<Cart> carts)
        {
            List<int> result = new List<int>();
            for(int i = 0; i < collections.Count; i++) 
            {
                for(int x = 0; x < carts.Count; x++)
                {
                    if(carts[x].ProductID != collections[i].ProductID)
                        result.Add(collections[i].ProductID);
                    else
                        continue;
                };
            };
            return result;
        }
        //public List<int> GettingFromCollectionDetails(List<CollectionDetails> collections)
        //{
        //    var AllCollectionDetails = new List<CollectionDetails>();
        //    for(var i = 0; i < collections.Count; i++)
        //    {
                
        //    }
                
        //    result.Add(collections[i].ProductID);
                    
            
        //    return result;
        //}
        public OrderViewModel Remove(int ID)
        {
            var filter = PredicateBuilder.New<Order>();
            filter = filter.Or(c => c.ID == ID);
            var query = base.GetByID(filter);
            return base.Remove(query).Entity.ToViewModel();
        }
        public OrderViewModel Delete(OrderViewModel model)
        {
            var filterd = PredicateBuilder.New<Order>();
            var old = filterd;
            filterd = filterd.Or(p => p.ID == model.ID);
            var res = base.GetByID(filterd);
            if (res.IsDeleted == false)
            {
                res.IsDeleted = true;
            }
            else res.IsDeleted = false;
            return res.ToViewModel();
        }



        public OrderViewModel EditOrderStatus(int ID)
        {
            var filter = PredicateBuilder.New<Order>();
            filter = filter.Or(p => p.ID == ID);
            var query = GetbyID(filter);
            if (query.OrderStatus == extrade.models.OrderStatus.pending)
            {
                query.OrderStatus = extrade.models.OrderStatus.accepted;
            }
            else if (query.OrderStatus == extrade.models.OrderStatus.rejected)
                query.OrderStatus = extrade.models.OrderStatus.accepted;

            else if (query.OrderStatus == extrade.models.OrderStatus.accepted)
                query.OrderStatus = extrade.models.OrderStatus.rejected;
            return base.Update(query).Entity.ToViewModel();
        }


    }
}
