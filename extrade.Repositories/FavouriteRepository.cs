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
    public class FavouriteRepository : GeneralRepositories<Favourite>
    {
        public FavouriteRepository(ExtradeContext _DBContext) : base(_DBContext)
        {
        }

        public PaginingViewModel<List<FavouriteViewModel>> GetFavourites(
                        string UserID = "", 
                        int ID = 0,
                        string OrderBy = "",
                        bool IsAscending = false,
                        int PageIndex = 1,
                        int PageSize = 20)
        {
            
                var Filtering = PredicateBuilder.New<Favourite>();
                var oldFiltering = Filtering;
                if (ID > 0)
                    Filtering = Filtering.Or(p => p.ID == ID);
            if (!string.IsNullOrEmpty(UserID))
                Filtering = Filtering.Or(p => p.UserID == UserID);
            if (oldFiltering == Filtering)
                    Filtering = null;
                var query = Get(Filtering, OrderBy, IsAscending, PageIndex, PageSize, null);

                var Result =
                    query.Select(p => new FavouriteViewModel()
                    {
                        ID = p.ID,
                        ProductID=p.ProductID,
                        NameEn=p.Product.NameEn,
                        NameAr=p.Product.NameAr,
                        image=p.Product.ProductImages.FirstOrDefault().image??"",
                        Description=p.Product.Description,
                        Price=p.Product.Price,
                        
                        UserID =p.UserID
                    }).Where(p=>p.UserID==UserID);

                PaginingViewModel<List<FavouriteViewModel>>
                    FinalResult = new PaginingViewModel<List<FavouriteViewModel>>()
                    {
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        Count = GetList().Count(),
                        Data = Result.Where(p=>p.UserID==UserID).ToList(),
                    };

                return FinalResult;

            
        }
        public FavouriteViewModel Add(FavouriteViewModel obj)
        {
            var added = new Favourite
            {
                ID = obj.ID,
                ProductID = obj.ProductID,
                UserID = obj.UserID
            };

            var add= base.Add(added).Entity;
            return new FavouriteViewModel
            {
                ID = add.ID,
                ProductID = add.ProductID,
                UserID = add.UserID
            };
        }
        public new Favourite Add(Favourite obj) {
            var query = base.GetList().Where(f => f.ProductID == obj.ProductID && f.UserID == obj.UserID);
            if (query.Count() <= 0)
            {
                return base.Add(obj).Entity;
            }
            else
                return base.Remove(obj).Entity;
        }
        public Favourite Remove(int ID)
        {
            var filter = PredicateBuilder.New<Favourite>();
            filter = filter.Or(p => p.ID == ID);
            var result = base.GetbyID(filter);
            return base.Remove(result).Entity;
        }
    }
}
