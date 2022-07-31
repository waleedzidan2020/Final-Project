using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using extrade.models;

namespace Extrade.ViewModels
{
    public static class FavouriteExtensions
    {
        public static Favourite ToModel(this FavouriteEditViewModel obj) =>
           new Favourite
           {
               ID = obj.ID,
               ProductID = obj.ProductID,
               UserID = obj.UserID,
           };
        public static FavouriteViewModel ToViewModel(this Favourite obj) =>
           new FavouriteViewModel
           {
               ID = obj.ID,
               UserID = obj.UserID,
               ProductID = obj.ProductID,
               NameEn = obj.Product.NameEn,
               NameAr = obj.Product.NameAr,
               Description = obj.Product.Description,
               Price = obj.Product.Price,

           };
        public static Favourite TVModel(this FavouriteViewModel model)
        {
            return new Favourite
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
            };
        }
    }

}
