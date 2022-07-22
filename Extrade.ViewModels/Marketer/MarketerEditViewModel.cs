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
    public static partial class MarketerExtension
    {

        public static Marketer ToModel(this MarketerEditViewModel model)
        {


            return new Marketer()
            {

                
                UserID = model.UserID,
                TaxCard = model.TaxCard,
                Salary = model.Salary,
                MarketerStatus = model.MarketerStatus,
                IsDeleted = model.IsDeleted,
                

            };


        }

    }
    public class MarketerEditViewModel
    {
        public string? UserID { get; set; }
        public float Salary { get; set; }
        
        public string TaxCard { get; set; }
        public MarketerStatus MarketerStatus { get; set; }

        public bool IsDeleted { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
