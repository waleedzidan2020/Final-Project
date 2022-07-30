using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;

namespace Extrade.ViewModels
{
    public static partial class CollectionExtensions
    {
        public static Collection ToModel(this CollectionEditViewModel model)
        {
            return new Collection
            {
                                 
                NameEN = model.NameEN,
                NameAr = model.NameAr,
                Code=model.Code,
                MarketerID=model.MarketerID,
                ID = model.ID,
                
                
            };
        }
    }
    public class CollectionEditViewModel
    {
        public int ID { get; set; }
        public string? MarketerID { get; set; }
        [Required(ErrorMessage = "Name Must Have Value")]
        public string? NameAr { get; set; }
        [Required(ErrorMessage = "Name Must Have Value")]
        public string? NameEN { get; set; }
        public string? Code { get; set; }
        public bool IsDeleted { get; set; }
    }
}
