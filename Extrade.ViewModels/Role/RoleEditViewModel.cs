using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class RoleEditViewModel
    {
        
        public string? ID { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
