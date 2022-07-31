using extrade.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public static class CartExtensions
    {
        public static Cart ToModel(this CartEditViewModel obj) =>
            new Cart
            {
                ID = obj.ID,
                ProductID = obj.ProductID,
                UserID = obj.UserID,
                Quantity = obj.Quantity,
            };
        public static CartViewModel ToViewModel(this Cart obj) =>
            new CartViewModel
            {
                ID = obj.ID,
                UserID = obj.UserID,
                ProductID = obj.ProductID,
                //NameEn=obj.Product.NameEn,
                //NameAr=obj.Product.NameAr,
                Quantity = obj.Quantity,
                //Description=obj.Product.Description,
                //Price=obj.Product.Price,

            };
        public static Cart TVModel(this CartViewModel model)
        {
            return new Cart
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
                //NameEn=obj.Product.NameEn,
                //NameAr=obj.Product.NameAr,
                Quantity = model.Quantity,
                
                //Description=obj.Product.Description,
                //Price=obj.Product.Price,
            };
        }
    }

    
}
