using extrade.models;
using Extrade.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.Repositories
{
    public class CartRepository : GeneralRepositories<Cart>
    {
        public CartRepository(ExtradeContext _DBContext) : base(_DBContext)
        {
        }
        public List<Cart> Get(string UserID)=>
            base.GetList().Where(p => p.UserID == UserID).ToList();
        public Cart GetByID(int ID)
        {
            var filter = PredicateBuilder.New<Cart>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);
            var query = base.GetByID(filter);
            return query;
        }

        public CartViewModel Add(CartEditViewModel obj) =>
            base.Add(obj.ToModel()).Entity.ToViewModel();
        public CartViewModel Update(CartEditViewModel obj)
        {
            var result= GetByID(obj.ID);
            result.Quantity = obj.Quantity;
            result.ProductID = obj.ProductID;

            return base.Update(result).Entity.ToViewModel();
        }
        
        public CartViewModel Remove(int ID)
        {
            var query = GetByID(ID);
            return base.Remove(query).Entity.ToViewModel();
        }
    }
}
