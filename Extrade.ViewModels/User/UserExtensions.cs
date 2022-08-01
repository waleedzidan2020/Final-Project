using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Presentation;
using extrade.models;

namespace Extrade.ViewModels
{
    public static class UserExtensions
    {
            public static UserViewModel ChangeUserToUserViewModel(this User obj) =>
            new UserViewModel
            {
                ID=obj.Id,
                UserName = obj.UserName,
                NameEn = obj.NameEn,
                NameAr = obj.NameAr,
                Password = obj.PasswordHash,
                Email = obj.Email,
                Country = obj.Country,
                City = obj.City,
                Street = obj.Street,
                IsDeleted=obj.IsDeleted,
                Phones = obj.PhoneNumber.Select(p=>p.Number).ToList(),
                Img = obj.Img
            };
        public static User ChangeUserLoginToUser(this UserLoginViewModel obj) =>
            new User
            {
                Email = obj.Email,
                PasswordHash = obj.Password,
               
            };

        public static User ToUserLogViewModel(this UserLoginViewModel obj) =>
          new User
          {
             
              Email = obj.Email,
              IsDeleted=obj.IsDeleted

          };

        //public static bool IsChildOf(this Control c, Control parent)
        //{
        //    return ((c.Parent != null && c.Parent == parent) || (c.Parent != null ? c.Parent.IsChildOf(parent) : false));
        //}
        public static UserControllersViewModel ChangeUserToUserControllersViewModel(this UserViewModel obj) =>
            new UserControllersViewModel
            {
                
                NameEn=obj.NameEn,
                NameAr = obj.NameAr,
                Password = obj.Password,
                UserName = obj.Email,
                Email = obj.Email,
                Country = obj.Country,
                City = obj.City,
                Street = obj.Street,
                IsDeleted=obj.IsDeleted,
               
                Img = obj.Img,
                
                
            };



        public static User ChangeUserToUserControllersViewModel(this UserControllersViewModel obj) =>
          new User
          {

              NameEn = obj.NameEn,
              NameAr = obj.NameAr,
              PasswordHash = obj.Password,
              UserName = obj.Email,
              Email = obj.Email,
              Country = obj.Country,
              City = obj.City,
              Street = obj.Street,
              IsDeleted = obj.IsDeleted,
              PhoneNumber = obj.Phones.Select(p => new Phone
              {
                  Number = p
              }).ToList(),
              Img = obj.Img,

          };
        public static UserControllersViewModel UserToEdit(this User obj) =>
            new UserControllersViewModel
            {

                NameEn = obj.NameEn,
                NameAr = obj.NameAr,
                Password = obj.PasswordHash,
                UserName = obj.Email,
                Email = obj.Email,
                Country = obj.Country,
                City = obj.City,
                Street = obj.Street,
                IsDeleted = obj.IsDeleted,
                Phones = obj.PhoneNumber.Select(p => p.Number).ToList(),
                Img = obj.Img,
                


            };

    }


}
