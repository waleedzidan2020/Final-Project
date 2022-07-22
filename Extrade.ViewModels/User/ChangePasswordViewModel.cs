using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string id { get; set; }
        [Required , StringLength(20,MinimumLength =6),DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required, StringLength(20, MinimumLength = 6), DataType(DataType.Password),Compare("ConfirmPassword")]
        public string NewPassword { get; set; }
        [Required, StringLength(20, MinimumLength = 6), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
