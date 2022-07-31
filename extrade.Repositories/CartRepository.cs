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
        public List<CartViewModel> Get(string UserID="")
        {
            var filter = PredicateBuilder.New<Cart>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty(UserID))
                filter = filter.Or(o => o.UserID == UserID);

            if (oldFilter == filter)
                filter = null;

            var Query = base.Get(filter);
            var Result = Query.Select(i => new CartViewModel
            {
                ID = i.ID,
                UserID = i.UserID,
                ProductID = i.ProductID,
                NameEn = i.Product.NameEn,
                NameAr = i.Product.NameAr,
                Quantity = i.Quantity,
                Description = i.Product.Description,
                Price = i.Product.Price,
                Images=i.Product.ProductImages.FirstOrDefault().image
            });
            return Result.ToList();
        }
            
        public CartViewModel? GetByID(int ID)
        {
            //var filter = PredicateBuilder.New<Cart>();
            //var old = filter;
            //if (ID > 0)
            //    filter = filter.Or(p => p.ID == ID);
            //var query = base.GetByID(filter);
            return base.GetList().Where(i => i.ID == ID).Select(i => new CartViewModel
            {
                ID =i.ID,
                UserID=i.UserID,
                ProductID=i.ProductID,
                NameEn=i.Product.NameEn,
                NameAr=i.Product.NameAr,
                Quantity = i.Quantity,
                Description=i.Product.Description,
                Price=i.Product.Price,
                Images= i.Product.ProductImages.FirstOrDefault().image
            }).FirstOrDefault();
        }

        public CartViewModel Add(CartEditViewModel obj) =>
            base.Add(obj.ToModel()).Entity.ToViewModel();
        public CartViewModel Update(CartEditViewModel obj)
        {
            var filter = PredicateBuilder.New<Cart>();
            filter = filter.Or(c => c.ID == obj.ID);
            var query = base.GetByID(filter);

            query.Quantity = obj.Quantity;;

            return base.Update(query).Entity.ToViewModel();
        }
        
        public CartViewModel Remove(CartViewModel obj)
        {
            //var query = GetByID(ID);
            Cart cart = obj.TVModel();
            return base.Remove(cart).Entity.ToViewModel();
        }
    }
}
