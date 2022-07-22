using extrade.models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class UserMarketerEditViewModel
    {
        public string? ID { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? UserName { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? NameEn { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? NameAr { get; set; }
        [Required, StringLength(40, MinimumLength = 7)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required, StringLength(40, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? Country { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? City { get; set; }
        [Required, StringLength(40, MinimumLength = 3)]
        public string? Street { get; set; }

        public string? Img { get; set; }
        public bool IsDeleted { get; set; }
        public string? UserID { get; set; }
        public float Salary { get; set; }

        public string TaxCard { get; set; }
        public MarketerStatus MarketerStatus { get; set; }
        public IFormFile? uploadedimg { get; set; }

        public List<string>? Phones { get; set; }
        public string? Role { get; set; }
    }
}
