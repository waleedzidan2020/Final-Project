using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using extrade.models;
using Extrade.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Extrade.Repositories
{
    public class MarketerRebository : GeneralRepositories<Marketer>
    {

        public readonly UserRepository UserRepo;
        public readonly CollectionRepository CollRepo;

        public readonly UnitOfWork unit;
        public readonly UserManager<User> UserMan;
        public MarketerRebository(UserManager<User> _UserMan, CollectionRepository CollRepo,UserRepository _UserRepo,ExtradeContext _DBContext,UnitOfWork _unit) : base(_DBContext)
        {
            unit = _unit;
            UserMan = _UserMan;
            UserRepo = _UserRepo;
            this.CollRepo = CollRepo;
        }
        public PaginingViewModel<List<MarketerViewModels>> Get( string? _UserID = null, string _TaxCard = "",
            float _Salary=0,string _CollectionNameEn = "",string _CollectionNameAr = "", 
                bool IsDeleted = false,
                string orderby = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {

            var filter = PredicateBuilder.New<Marketer>();
            var oldFiler = filter;

            if (_UserID != null)
                filter = filter.Or(p => p.UserID.Contains(_UserID));
            if (!string.IsNullOrEmpty(_TaxCard))
                filter = filter.Or(p => p.TaxCard.Contains(_TaxCard));
            if (!string.IsNullOrEmpty(_CollectionNameEn))
                filter = filter.Or(p => p.Collections.Any(p=> p.NameEN.Contains(_CollectionNameEn)));
            if (!string.IsNullOrEmpty(_CollectionNameAr))
                filter = filter.Or(p => p.Collections.Any(p => p.NameAr.Contains(_CollectionNameAr)));
            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize,null);

            var Result =
            query.Select(i => new MarketerViewModels()
            {
                UserID = i.UserID,
                CollectionsCode=i.Collections.Select(p=>p.Code).ToList(),
                CollectionsName = i.Collections.Select(p=>p.NameEN).ToList(),
                TaxCard = i.TaxCard ,
                NameEn = i.User.NameEn,
                NameAr = i.User.NameAr,
                Email = i.User.Email,
                Country = i.User.Country,
                City = i.User.City,
                Street = i.User.Street,
                IsDeleted=i.IsDeleted
            });


            PaginingViewModel<List<MarketerViewModels>> finalResult = new PaginingViewModel<List<MarketerViewModels>>()
            {

                Count = base.GetList().Count(),
                Data = Result.ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };


            return finalResult;


        }


        public MarketerViewModels GetOne(string _id = "")
        {

            var filterd = PredicateBuilder.New<Marketer>();


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



        public Marketer GetOneMarketer(string _id = "")
        {

            var filterd = PredicateBuilder.New<Marketer>();


            var old = filterd;

            if (!string.IsNullOrEmpty(_id))
                filterd = filterd.Or(p => p.UserID == _id);


            if (old == filterd)
                filterd = null;


            var query = base.GetByID(filterd);

            if (query != null)
                return query;
            else
                return null;




        }
        public async Task<Marketer> Add(UserMarketerEditViewModel model)
        {
            var user=new UserControllersViewModel
            {
                City = model.City,
                Email = model.Email,
                UserName = model.Email,
                Country = model.Country,
                Password=model.Password,
                IsDeleted = model.IsDeleted,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                Phones = model.Phones.ToList(),
                Street = model.Street,
                Img = model.Img,
                Role =model.Role
            };
            var how = await UserRepo.Add(user);
            if (how.Succeeded)
            {
                var userid = UserRepo.GetByEmails(model.Email).Id;
               var res= base.Add(new Marketer
                {
                    UserID = userid,
                    TaxCard = model.TaxCard,

                }).Entity;
                return res;
            }
            else
                return new Marketer();

             

        }
        public MarketerViewModels Update(MarketerEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Marketer>();
            var old = filterd;
            filterd = filterd.Or(p => p.UserID == model.UserID);
            var res = base.GetByID(filterd);
            res.TaxCard = model.TaxCard;
            return res.ToViewModel();
        }
        public MarketerViewModels UpdateSalary(MarketerViewModels model)
        {
            var filterd = PredicateBuilder.New<Marketer>();
            var old = filterd;
            filterd = filterd.Or(p => p.UserID == model.UserID);
            var res = base.GetByID(filterd);
            res.Salary = model.Salary;
            return res.ToViewModel();
        }

        public MarketerViewModels Remove(MarketerEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Marketer>();
            var old = filterd;

            filterd = filterd.Or(p => p.UserID == model.UserID);


            var res = base.GetByID(filterd);


            if (res.IsDeleted == false)
            {
                res.IsDeleted = true;
            }
            else res.IsDeleted = false;

            return res.ToViewModel();


        }





        public async Task<Marketer> Delete(string id)
        {

            var filterd = PredicateBuilder.New<Marketer>();
            var old = filterd;

            filterd = filterd.Or(p => p.UserID == id);


            var res = base.GetByID(filterd);


            if (res.IsDeleted == false)
            {
                res.IsDeleted = true;
            }
            else res.IsDeleted = false;

            return res;


        }
        public MarketerViewModels Add(MarketerEditViewModel model)
        {
            Marketer marketer = model.ToModel();
            return base.Add(marketer).Entity.ToViewModel();
        }

        public MarketerViewModels AcceptMarketer(string ID)
        {
            var filter = PredicateBuilder.New<Marketer>();
            filter = filter.Or(p => p.UserID == ID);
            var query = GetbyID(filter);
            if (query.MarketerStatus == MarketerStatus.pending)
            {
                query.MarketerStatus = MarketerStatus.accebted;
            }
            else if (query.MarketerStatus == MarketerStatus.rejected)
                query.MarketerStatus = MarketerStatus.accebted;

            else query.MarketerStatus = MarketerStatus.rejected;
            return base.Update(query).Entity.ToViewModel();
        }

        public MarketerViewModels RejectMarketer(string ID)
        {
            var filter = PredicateBuilder.New<Marketer>();
            filter = filter.Or(p => p.UserID == ID);
            var query = GetbyID(filter);
            if (query.MarketerStatus == MarketerStatus.pending)
            {
                query.MarketerStatus = MarketerStatus.rejected;
            }
            else if (query.MarketerStatus == MarketerStatus.accebted)
                query.MarketerStatus = MarketerStatus.rejected;

            else query.MarketerStatus = MarketerStatus.accebted;
            return base.Update(query).Entity.ToViewModel();
        }

        public MarketerViewModels GetOneByCollection(int id =0)
        {
            var collection = new CollectionViewModel();
            if (id >0)
            {
               collection = CollRepo.GetOne(id);
            }
            var filterd = PredicateBuilder.New<Marketer>();
            var old = filterd;
            filterd = filterd.Or(p => p.UserID == collection.MarketerID);

            if (old == filterd)
                filterd = null;

            var query = base.GetByID(filterd);

            if (query != null)
                return query.ToViewModel();
            else
                return null;
        }
    }

   
}


