using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
using Extrade.ViewModels;

namespace Extrade.Repositories
{
    public class RoleRepository : GeneralRepositories<IdentityRole>
    {

        UserManager<User> UserManager;
        RoleManager<IdentityRole> RoleManager;
        public RoleRepository(ExtradeContext context,
            RoleManager<IdentityRole> _RoleManager,
            UserManager<User> _UserManager) : base(context)
        {
            RoleManager = _RoleManager;
            UserManager = _UserManager;
        }

        public async Task<IdentityResult> AddRole(RoleEditViewModel obj) =>
           await RoleManager.CreateAsync(obj.ChangeRoleEditToRole());

        public async Task<string> GetRole(RoleEditViewModel obj) =>
           await RoleManager.GetRoleIdAsync(obj.ChangeRoleEditToRole());

        public new List<TextValueViewModel> GetList() =>

           base.GetList().Select(p => new TextValueViewModel
           {
               Text = p.Name



           }).ToList();





        public TextValueViewModel GetVendorRole()
        {
            var res = base.GetList().Where(p => p.Name == "Vendor").ToList();

            return res.Select(p => new TextValueViewModel
            {
                Text = p.Name

            }).FirstOrDefault();


        }
        public TextValueViewModel GetUserRole()
        {
            var res = base.GetList().Where(p => p.Name == "User").ToList();

            return res.Select(p => new TextValueViewModel
            {
                Text = p.Name
            }).FirstOrDefault();
        }


        public TextValueViewModel GetMarketerRole()
        {
            var res = base.GetList().Where(p => p.Name == "Marketer").ToList();

            return res.Select(p => new TextValueViewModel
            {
                Text = p.Name



            }).FirstOrDefault();


        }
    }
}

        //public async Task<IdentityResult> UpdateRole(UserControllersViewModel obj, string Role)
        //{
        //    var result = obj.ChangeUserToUserControllersViewModel();
        //    return await UserManager.AddToRoleAsync(result, Role);
        //}