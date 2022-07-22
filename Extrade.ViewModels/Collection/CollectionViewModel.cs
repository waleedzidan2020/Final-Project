using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;
namespace Extrade.ViewModels
{
    public static partial class CollectionExtensions
    {
        public static CollectionViewModel ToViewModel(this Collection model)
        {
            return new CollectionViewModel
            {
                NameEN = model.NameEN,
                NameAr = model.NameAr,
                Code=model.Code,
                ID=model.ID,
                MarketerID=model.MarketerID
            };
        }
    }
    public class CollectionViewModel
    {
        public int ID { get; set; }
        public string? MarketerID { get; set; }
        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEN { get; set; }
        public bool IsDeleted { get; set; }
        public List<int>? Products { get; set; }
    }
}
