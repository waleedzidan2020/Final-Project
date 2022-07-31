using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using LinqKit;
using Extrade.ViewModels;
using Extrade.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using Microsoft.AspNetCore.Mvc;

namespace Extrade.Repositories
{
    public class CollectionRepository : GeneralRepositories<Collection>
    {
        public CollectionRepository(ExtradeContext _Contex)

           : base(_Contex)
        {}
        public PaginingViewModel<List<CollectionViewModel>> Get(string UserId ="",string NameAr = "", 
            string? NameEN = "", string Namepenroduct = "", 
            string Namearproduct = "",string Description="", float Price = 0,
            int Quantity = 0, ProductStatus? Status = null,
                 string? orderby = null,
            bool IsAsceding = false, int pageindex = 1, int pagesize = 20)
        {

            var filter = PredicateBuilder.New<Collection>();
            var old = filter;
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(p => p.NameAr.Contains(NameAr));
            if (!string.IsNullOrEmpty(UserId))
                filter = filter.Or(p => p.MarketerID == UserId);
            if (!string.IsNullOrEmpty(NameEN))
                filter = filter.Or(p => p.NameEN.Contains(NameEN));
            if (!string.IsNullOrEmpty(Namepenroduct))
                filter = filter.Or(p => p.CollectionDetails.Any(p=>p.Product.NameAr==Namearproduct));
            if (!string.IsNullOrEmpty(Namearproduct))
                filter = filter.Or(p => p.CollectionDetails.Any(p => p.Product.NameAr == Namearproduct));
            if (!string.IsNullOrEmpty(Description))
                filter = filter.Or(p => p.CollectionDetails.Any(p => p.Product.Description == Description));
            if (Price>0)
                filter = filter.Or(p => p.CollectionDetails.Any(p => p.Product.Price == Price));
            if (Quantity > 0)
                filter = filter.Or(p => p.CollectionDetails.Any(p => p.Product.Quantity == Quantity));
            if (Status != null)
                filter = filter.Or(p => p.CollectionDetails.Any(p => p.Product.Status == Status));
            if (old == filter)
                filter = null;
            var query= base.Get(filter, orderby, IsAsceding, pageindex, pagesize,"CollectionDetails");
            var Result = query.Select(p => new CollectionViewModel()
            {
                NameAr = p.NameAr,
                NameEN = p.NameEN,
                Code=p.Code,
                ID=p.ID,
            }).Where(p => p.MarketerID == UserId);

            PaginingViewModel<List<CollectionViewModel>> finalResult = new PaginingViewModel<List<CollectionViewModel>>() {

                Count = base.GetList().Count(),
                Data = Result.Where(p=>p.MarketerID==UserId).ToList(),
                PageIndex= pageindex,
                PageSize= pagesize

            };

            return finalResult;


        }
        public CollectionViewModel GetOne(int ID = 0)
        {

            var filter = PredicateBuilder.New<Collection>();
            var old = filter;
            if (ID > 0)
                filter = filter.Or(p => p.ID == ID);
            if (filter == old)
                filter = null;

            var res = base.GetbyID(filter);
            return res.ToViewModel();
        }
        public Collection GetOneByCode(string Code)
        {

            var filter = PredicateBuilder.New<Collection>();
            var old = filter;
            if (Code!=null)
                filter = filter.Or(p => p.Code == Code);
            if (filter == old)
                filter = null;

            var res = base.GetbyID(filter);
            return res;
        }

        public List<Collection> GetWhereMarketerID(string ID)
        {
            
            var query = base.GetList().Select(p => new Collection
            {
                ID=p.ID,
                MarketerID = p.MarketerID,
                NameEN = p.NameEN,
                NameAr=p.NameAr,
                Code = p.Code
            }).Where(p => p.MarketerID == ID);
            return query.ToList();
        }
        public IPagedList<CollectionViewModel>Search(int pageindex = 1, int pagesize = 20)
        =>
            GetList().Select(p => new CollectionViewModel
            {
                NameAr = p.NameAr,
                NameEN = p.NameEN

            }).ToPagedList(pageindex, pagesize);
        


        public CollectionViewModel Add(CollectionEditViewModel model)
        {
            Collection collection = model.ToModel();
            return base.Add(collection).Entity.ToViewModel();
        }

        public CollectionViewModel Update(CollectionEditViewModel model)
        {

            Collection collection = model.ToModel();
            return base.Update(collection).Entity.ToViewModel();
        }
        public CollectionViewModel Delete(CollectionEditViewModel obj)
        {
            return base.Remove(obj.ToModel()).Entity.ToViewModel();
        }
    }
}
