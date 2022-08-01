using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;



namespace Extrade.ViewModels
{
    public static partial class MarketerExtension
    {
        public static MarketerViewModels ToViewModel(this Marketer model)
        {
            return new MarketerViewModels()
            {
                TaxCard = model.TaxCard,
                Salary = model.Salary,
                IsDeleted = model.IsDeleted,
                Status = model.MarketerStatus,
                UserID = model.UserID,






            };
        }





        public static MarketerEditViewModel ToEditViewModel(this MarketerViewModels model)
        {


            return new MarketerEditViewModel()
            {

                UserID = model.UserID,
                TaxCard = model.TaxCard,
                Salary = model.Salary,
                IsDeleted = model.IsDeleted

            };
        }
    }
    public class MarketerViewModels
    {
       
        public string? UserID { get; set; }
        public string TaxCard { get; set; }
        public float Salary { get; set; }
        public bool IsDeleted { get; set; }
        //user
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        public MarketerStatus Status { get; set; }
        public List<string>? CollectionsCode { get; set; }
        public List<string>? CollectionsName { get; set; }






    }
}
