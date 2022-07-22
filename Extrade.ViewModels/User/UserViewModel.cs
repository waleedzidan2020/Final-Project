using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class UserViewModel
    {
          public string? ID { get; set; }
          public string? UserName { get; set; }
          public string? NameEn { get; set; }
          public string? NameAr { get; set; }
          public string? Email { get; set; }
          public string? Password { get; set; }
          public string? Country { get; set; } 
          public string? City { get; set; } 
          public string? Street { get; set; }
          public string? Img { get; set; }
          public bool IsDeleted { get; set; }
          public string? Phone { get; set; }
          public List<string>? Phones { get; set; }
                
    }
}

