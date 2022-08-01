using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Extrade.ViewModels;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Extrade.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Extrade.Repositories
{
    public class UserRepository : GeneralRepositories<User>
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        IConfiguration Configuration;
        public UserRepository(ExtradeContext context, UserManager<User> _UserManager,
            SignInManager<User> _SignInManager, IConfiguration _Configuration) : base(context)
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
            Configuration = _Configuration;
        }
        public PaginingViewModel<List<UserViewModel>> Get(string? _ID = null, string? NameEn = null, string? NameAr = null,
                string? Country = null, string? City = null, string? Street = null, bool? IsDeleted = false, string orderby = "ID", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {
            var filter = PredicateBuilder.New<User>();
            var oldFiler = filter;
            if (_ID != null)
                filter = filter.Or(p => p.Id.Contains(_ID));
            if (!string.IsNullOrEmpty(NameEn))
                filter = filter.Or(p => p.UserName.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(p => p.Country.Contains(NameAr));
            if (!string.IsNullOrEmpty(Country))
                filter = filter.Or(p => p.Country.Contains(Country));
            if (IsDeleted != null)
                filter = filter.Or(p => p.IsDeleted == IsDeleted);
            if (!string.IsNullOrEmpty(City))
                filter = filter.Or(p => p.City.Contains(City));
            if (!string.IsNullOrEmpty(Street))
                filter = filter.Or(p => p.Street.Contains(Street));

            if (filter == oldFiler)
                filter = null;
            var query = Get(filter, orderby, isAscending, pageIndex, pageSize, null);

            var result =
            query.Select(i => new UserViewModel()
            {
                ID = i.Id,
                NameEn = i.NameEn,
                NameAr = i.NameAr,
                Email = i.Email,
                Password = i.PasswordHash,
                Country = i.Country,
                City = i.City,
                Street = i.Street,
                IsDeleted = i.IsDeleted,
                Phones = i.PhoneNumber.Select(p => p.Number).ToList(),
                Img = i.Img

            });

            PaginingViewModel<List<UserViewModel>>
                finalResult = new PaginingViewModel<List<UserViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };

            return finalResult;
        }
        public UserViewModel GetByID(string ID)
        {
            var filter = PredicateBuilder.New<User>();
            if (ID != null)
                filter = filter.Or(u => u.Id == ID);

            var query = GetByID(filter);

            var result = query?.ChangeUserToUserViewModel();

            return result;
        }
        public User GetUserByID(string ID)
        {
            var filter = PredicateBuilder.New<User>();
            if (ID != null)
                filter = filter.Or(u => u.Id == ID);

            var query = GetByID(filter);

            
            return query;
        }



        public UserViewModel GetByEmail(string Email)
        {
            var filter = PredicateBuilder.New<User>();
            if (Email != null)
                filter = filter.Or(u => u.Email == Email);

            var query = GetByID(filter);

            var result = query?.ChangeUserToUserViewModel();

            return result;
        }

        public User GetByEmails(string Email)
        {
            var filter = PredicateBuilder.New<User>();
            if (Email != null)
                filter = filter.Or(u => u.Email == Email);

            var query = GetByID(filter);

            return query;
        }


        //public string getUserID()
        //{
        //    UserManager.
        //}

        public async Task<string> SignIn(UserLoginViewModel obj) { 
         var result = await SignInManager.PasswordSignInAsync(obj.ChangeUserLoginToUser().Email,
              obj.Password,obj.RememberMe,true);
            if (result.Succeeded)
            {
                var User = await UserManager.FindByEmailAsync(obj.ChangeUserLoginToUser().Email);
                List<Claim> Claims= new List<Claim>();
                IList<string> role= await UserManager.GetRolesAsync(User);

                Claims.Add(new Claim(User.Id,User.Id));
                role.ToList().ForEach(r => Claims.Add(new Claim(ClaimTypes.Role, r)));
                JwtSecurityToken Token = new JwtSecurityToken(signingCredentials: new SigningCredentials
                    (
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTKey:Key"])),
                        SecurityAlgorithms.HmacSha256
                    ), expires: DateTime.Now.AddDays(7),
                       claims: Claims
                    );
              return  new JwtSecurityTokenHandler().WriteToken(Token);
            }
            return string.Empty;
        }
        public async Task<SignInResult> SignInAsMVc(UserLoginViewModel obj)
        {



            return await SignInManager.PasswordSignInAsync(obj.Email,
                   obj.Password, obj.RememberMe
                   , true);






        }
        public async Task SignOut() =>
            await SignInManager.SignOutAsync();
        public async Task<IdentityResult> Add(UserControllersViewModel obj)
        {
            try
            {

                var result = obj.ChangeUserToUserControllersViewModel();
                await UserManager.CreateAsync(result, obj.Password);
                var res = await UserManager.AddToRoleAsync(result, obj.Role);
                return res;
            }catch(Exception e)
            {
                return new IdentityResult ();
            }
          
        }
        public async Task<IdentityResult> Update(UserControllersViewModel result)
        {
            var filter = PredicateBuilder.New<User>();
            filter = filter.Or(p => p.Id == result.ID);
            var last = GetByID(filter);
            last.UserName = result.Email;
            last.NameEn = result.NameEn;
            last.NameAr = result.NameAr;
            last.Email = result.Email;
            last.Country = result.Country;
            last.City = result.City;
            last.Street = result.Street;
            if (result.Img != null)
            {
                last.Img = result.Img;
            }
            //last.PhoneNumber = result.Phones.Select(p =>new Phone
            //{
            //    Number=p
            //}).ToList();
            
            return await UserManager.UpdateAsync(last);
        }
        public async Task<IdentityResult> Delete(string obj)
        {
            var filter = PredicateBuilder.New<User>();
            filter = filter.Or(p => p.Id == obj);
            var last = GetByID(filter);
            if (last.IsDeleted == false)
            {
                last.IsDeleted = true;
            }
            else last.IsDeleted = false;
            return await UserManager.UpdateAsync(last);
        }
        public async Task<IdentityResult> AddRoleToUser(string user)
        {
            
            var filter = PredicateBuilder.New<User>();
            filter = filter.Or(p => p.Id == user);
            var last = GetByID(filter);
            return await UserManager.AddToRoleAsync(last, "admin");
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model) =>
        await UserManager.ChangePasswordAsync(await UserManager.FindByIdAsync(model.id), model.CurrentPassword, model.NewPassword);





    }
}

