using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using extrade.models;
using LinqKit;
using Extrade.ViewModels;
using Extrade.Repositories;

namespace Extrade.Repositories
{
    public class VendorRepository : GeneralRepositories<Vendor>
    {
        private readonly ProductRepository ProductRepo;
        public VendorRepository(ExtradeContext _DBContext
            ,ProductRepository _ProductRepo
            ) : base(_DBContext) 
        {
            ProductRepo = _ProductRepo;
        }

        //public VendorViewModel GetOne(string _id = null)
        //{

        //    var filterd = PredicateBuilder.New<Vendor>();
        //    var old = filterd;

        //    if (!String.IsNullOrEmpty(_id))
        //        filterd = filterd.Or(p => p.UserID.Contains(_id));
        //    if (old == filterd)
        //        filterd = null;
        //    var query = base.Get(filterd, null, false, 1, 1, null);
        //    var res = query.Select(p => new VendorViewModel()
        //    {

        //        UserID = p.UserID,
        //        BrandNameAr = p.BrandNameAr,
        //        BrandNameEr = p.BrandNameEr,
        //        //VendorImage = p.VendorImage.Select(p => p.Image).ToList(),
        //        //Product = p.Product.Select(p => p.NameEn).ToList(),
        //        NameAr = p.User.NameAr,
        //        Email = p.User.Email,
        //        Country = p.User.Country,
        //        City = p.User.City,
        //        Street=p.User.Street

        //    }).FirstOrDefault();

        //    return res;


        //}



        public PaginingViewModel<List<VendorViewModel>> GetList(string _id = null, string _BrandNameAr = "", 
            string _BrandNameEn = "", string _NameProductAr = "", string _NameProductEn = "",
            bool IsDeleted=false,  string orderby = null, bool IsAsceding = false, int pageindex = 1,
            int pagesize = 20)
        {
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            if (!String.IsNullOrEmpty(_id))
                filterd = filterd.Or(p => p.UserID.Contains(_id));
            if (!string.IsNullOrEmpty(_BrandNameAr))
                filterd = filterd.Or(p => p.BrandNameAr .Contains(_BrandNameAr));
            if (!string.IsNullOrEmpty(_BrandNameEn))
                filterd = filterd.Or(p => p.BrandNameEr.Contains(_BrandNameEn));
            if (!string.IsNullOrEmpty(_NameProductAr))
                filterd = filterd.Or(p => p.Product.Any(p => p.NameAr.Contains(_NameProductAr)));
            if (!string.IsNullOrEmpty(_NameProductEn))
                filterd = filterd.Or(p => p.Product.Any(p => p.NameEn.Contains(_NameProductEn)));
            if (IsDeleted=false)
                filterd = filterd.Or(p => p.IsDeleted==IsDeleted);

            if (old == filterd)
                filterd = null;

            var query = base.Get(filterd, orderby, IsAsceding, pageindex, pagesize, null);
            var res = query.Select(p => new VendorViewModel()
            {

                UserID = p.UserID,
                BrandNameAr = p.BrandNameAr,
                BrandNameEr = p.BrandNameEr,
                VendorImage = p.VendorImage.Select(p => p.Image).ToList(),
                Product = p.Product.Select(p => p.NameEn).ToList(),
                NameAr = p.User.NameAr,
                Email = p.User.Email,
                Country = p.User.Country,
                City = p.User.City,
                Street = p.User.Street,
            });


            PaginingViewModel<List<VendorViewModel>> final = new PaginingViewModel<List<VendorViewModel>>()
            {
                Count = base.GetList().Count(),
                PageIndex = pageindex,
                PageSize = pagesize,
                Data = res.ToList()
            };

            return final;

        }



        public string GetVendorbyProductID(int ID)
        {
            var Filter = PredicateBuilder.New<Product>();
            Filter = Filter.Or(p => p.ID == ID);
            var result = ProductRepo.GetbyID(Filter);
            return result.VendorID;

        }
        public VendorViewModel GetOne(string _id = "")
        {

            var filterd = PredicateBuilder.New<Vendor>();


            var old = filterd;

            if (!string.IsNullOrEmpty(_id))
                filterd = filterd.Or(p => p.UserID == _id);


            if (old == filterd)
                filterd = null;


            var query = base.GetByID(filterd);

            if (query != null)
                return query.ToViewModel();
            else
                return null;

           


        }

        public VendorViewModel Update(VendorEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;

            filterd = filterd.Or(p => p.UserID == model.UserId);

            var res = base.GetByID(filterd);
            res.BrandNameAr = model.BrandNameAr;
            res.BrandNameEr = model.BrandNameEr;
            res.VendorImage.Clear();
            res.VendorImage = model.VendorImage.Select(p => new VendorImage()
            {

                Image = p

            }).ToList();
            
            return res.ToViewModel();


        }



        public VendorViewModel Remove(VendorEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;

            filterd = filterd.Or(p => p.UserID == model.UserId);


            var res = base.GetByID(filterd);
           
            res.IsDeleted =true;

            return res.ToViewModel();


        }



        public VendorViewModel Add(VendorEditViewModel model) {
            Vendor ven = model.ToModel();
            
            return base.Add(ven).Entity.ToViewModel();
        }
        public VendorViewModel AcceptVendor(string ID)
        {
            var filter = PredicateBuilder.New<Vendor>();
            filter = filter.Or(p => p.UserID == ID);
            var query = GetbyID(filter);
            if (query.VendorStatus == VendorStatus.pending)
            {
                query.VendorStatus = VendorStatus.accepted;
            }
            else if (query.VendorStatus ==VendorStatus.rejected)
                query.VendorStatus = VendorStatus.accepted;

            else query.VendorStatus = VendorStatus.rejected;
            return base.Update(query).Entity.ToViewModel();
        }
        public VendorViewModel RejectVendor(string ID)
        {
            var filter = PredicateBuilder.New<Vendor>();
            filter = filter.Or(p => p.UserID == ID);
            var query = GetbyID(filter);
            if (query.VendorStatus == VendorStatus.pending)
            {
                query.VendorStatus = VendorStatus.rejected;
            }
            else if (query.VendorStatus == VendorStatus.accepted)
                query.VendorStatus = VendorStatus.rejected;

            else query.VendorStatus = VendorStatus.accepted;
            return base.Update(query).Entity.ToViewModel();
        }
    }
}
