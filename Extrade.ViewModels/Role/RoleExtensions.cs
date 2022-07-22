using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public static class RoleExtensions
    {
        public static IdentityRole ChangeRoleEditToRole(this RoleEditViewModel obj)
        => new IdentityRole
        {
            Name=obj.Name
        };

        
    }
}
