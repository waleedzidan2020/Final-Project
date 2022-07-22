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
    public class CategoryRepository :GeneralRepositories<Category>

    {
        public CategoryRepository(ExtradeContext _DBContext) :base (_DBContext) { }
        public PaginingViewModel<List<CategoryViewModel>> Get(int _ID=0 , string? NameAr="", string? NameEn="", string? orderby = null,
            bool IsAsceding = false, int pageindex = 1, int pagesize = 20) {



            var filterd = PredicateBuilder.New<Category>();
            var old = filterd;
            if (_ID > 0)
                filterd = filterd.Or(i => i.ID == _ID);
            if (!string.IsNullOrEmpty(NameAr))
                filterd = filterd.Or(i => i.NameAr.Contains(NameAr));
            if (!string.IsNullOrEmpty(NameEn))
                filterd = filterd.Or(i => i.NameAr.Contains(NameEn));

            if (old == filterd)
                filterd = null;

         var query=   base.Get(filterd, orderby, IsAsceding, pageindex, pagesize,null);
         var res=   query.Select(p => new CategoryViewModel()
            {
             CatID=p.ID,
                NameAr=p.NameAr,
                NameEn=p.NameEn,
                Image=p.Image,
                NameProductsAr=p.Product.Select(p=>p.NameAr).ToList(),
                NameProductsEn = p.Product.Select(p => p.NameEn).ToList(),
                Quantity= p.Product.Select(p => p.Quantity).ToList(),
                Price = p.Product.Select(p => p.Price).ToList(),
                
                
                
            });

            PaginingViewModel<List<CategoryViewModel>> final = new PaginingViewModel<List<CategoryViewModel>>()
            {

                Count = base.GetList().Count(),
                Data = res.ToList(),
                PageIndex= pageindex,
                PageSize= pagesize

            };


            return final;


        }


        public CategoryViewModel GetOne(int _ID = 0)
        {



            var filterd = PredicateBuilder.New<Category>();
            var old = filterd;
            if (_ID > 0)
                filterd = filterd.Or(p => p.ID == _ID);

            if (old == filterd)
                filterd = null;

            var query = base.GetByID(filterd);
            return query.ToviewModel();
        }

        public CategoryViewModel Add(CategoryEditViewModel model) {
            Category cat = model.ToModel();
            return base.Add(cat).Entity.ToviewModel();
        
        }
        public CategoryViewModel Update(CategoryEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Category>();
            filterd = filterd.Or(c => c.ID == model.ID);
            var toupdatedCat = base.GetByID(filterd);
            toupdatedCat.NameEn = model.NameEn;
            toupdatedCat.NameAr = model.NameAr;
            toupdatedCat.Image = model.Image;
            return base.Update(toupdatedCat).Entity.ToviewModel();
        }



        public CategoryViewModel Remove(CategoryEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Category>();
            filterd = filterd.Or(c => c.ID == model.ID);
            var toupdatedCat = base.GetByID(filterd);
           
            return base.Remove(toupdatedCat).Entity.ToviewModel();

        }
    }
}
