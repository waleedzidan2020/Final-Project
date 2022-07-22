using extrade.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Extrade.MVC.Helpers
{
    public class UserClaims : UserClaimsPrincipalFactory<User,IdentityRole>
    {
        UserManager<User> UsManager;
        public UserClaims(UserManager<User> _UserManager,
            IOptions<IdentityOptions> optionsAccessor
            ,RoleManager<IdentityRole> Role
            ) : base(_UserManager, Role, optionsAccessor)
        {
            UsManager = _UserManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims= await base.GenerateClaimsAsync(user);
            var roles= await UsManager.GetRolesAsync(user);
            List<string> result = roles.ToList();
            
            foreach (string i in result)
            claims.AddClaim(new Claim(i, i));
            

            return claims;
        }
    }
}
