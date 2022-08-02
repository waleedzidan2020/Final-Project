using extrade.models;
using Extrade.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Extrade.Repositories
{
    public class CollectionDetailsRepository : GeneralRepositories<CollectionDetails>
    {
        public ProductRepository prodrep;
        public CollectionRepository Collrep;
        public CollectionDetailsRepository(ExtradeContext _Contex ,CollectionRepository _collrep)

           : base(_Contex)
        {
            prodrep = new ProductRepository(_Contex);
            Collrep = _collrep;
        }
        public PaginingViewModel<List<CollactionDetailsViewModel>> Get(int ProductID = 0,
             int CollectionID = 0, string Collection = "", string Product = "",
             string orderby = "ID",
        bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<CollectionDetails>();
            var oldfilter = filter;
            if (ProductID > 0)
                filter = filter.Or(p => p.ProductID == ProductID);
            if (CollectionID > 0)
                filter = filter.Or(p => p.CollectionID == CollectionID);
            if (!string.IsNullOrEmpty(Collection))
                filter = filter.Or(p => p.Collection.NameEN.Contains(Collection));
            if (!string.IsNullOrEmpty(Product))
                filter = filter.Or(p => p.Product.NameEn.Contains(Product));
            if (oldfilter == filter)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize,
           "Collection", "Product");

            var result =
            query.Select(i => new CollactionDetailsViewModel
            {
                ProductID = i.ProductID,
                CollectionID = i.CollectionID
            });
            PaginingViewModel<List<CollactionDetailsViewModel>>
          finalResult = new PaginingViewModel<List<CollactionDetailsViewModel>>()
          {
              PageIndex = pageIndex,
              PageSize = pageSize,
              Count = base.GetList().Count(),
              Data = result.ToList()
          };
            return finalResult;
        }
        public IPagedList<CollactionDetailsViewModel> Search(int pageIndex = 1, int pageSize = 20)
        =>
            GetList().Select(p => new CollactionDetailsViewModel
            {
                ProductID = p.ProductID,
                CollectionID = p.CollectionID
            }).ToPagedList(pageIndex, pageSize);

        public ProductViewModel GetProductByID(int ID)
        {
            var filter = PredicateBuilder.New<Product>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);

            var query = prodrep.GetbyID(filter);
            return query.ToViewModel();
        }
        public CollectionViewModel GetCollectionByID(int ID)
        {
            var filter = PredicateBuilder.New<Collection>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);
            var query = Collrep.GetbyID(filter);
            return query.ToViewModel();
        }
        public CollactionDetailsViewModel GetCollectionDetailsByID(int ID)
        {
            var filter = PredicateBuilder.New<CollectionDetails>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.CollectionID == ID);

            var query =  GetbyID(filter);
            return query.ToViewModel();
        }
        public CollactionDetailsViewModel Add(CollectionDetalisEditViewModel obj)
        {
            
            return base.Add(obj.ToModel()).Entity.ToViewModel();
        }
        
        public CollactionDetailsViewModel Delete(CollectionDetalisEditViewModel obj)
        {
            
            return base.Remove(obj.ToModel()).Entity.ToViewModel();
        }
        public List<ProductViewModel> GetProductsByCode(string Code)
        {
            var collection = Collrep.GetOneByCode(Code);
            var details = base.GetList().Where(p => p.CollectionID == collection.ID).ToList();
            var ProductList = new List<int>();
            foreach(var i in details)
            {
                ProductList.Add(i.ProductID);
            }
            var Products = new List<ProductViewModel>();
            foreach(var p in ProductList)
            {
                var query = prodrep.GetProductByID(p);
                Products.Add(query);
            }
            return Products;
        }
        
    }
}
