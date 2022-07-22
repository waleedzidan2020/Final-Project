using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace extrade.models
{
    public class MapperRelationships
    {
        public static void MapeRelationships(ModelBuilder builder)
        {
            //user=>phone
            builder.Entity<User>()
                 .HasMany(p => p.PhoneNumber).
                 WithOne(p => p.User).
                 HasForeignKey(p => p.UserID).
                 OnDelete(DeleteBehavior.NoAction);

            // user => vendor
            builder.Entity<User>()
                .HasOne(v=>v.Vendor)
                .WithOne(v => v.User)
                .HasForeignKey<Vendor>(v=>v.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            //user => Favourite
            builder.Entity<User>()
                .HasMany(f => f.Favourites)
                .WithOne(u => u.User)
                .HasForeignKey(f => f.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // User => Rating
            builder.Entity<User>()
                 .HasMany(f => f.Ratings)
                 .WithOne(u => u.User)
                 .HasForeignKey(f => f.UserID)
                 .OnDelete(DeleteBehavior.NoAction);
            // User => Marketer
            builder.Entity<User>()
                .HasOne(m => m.Marketer)
                .WithOne(u => u.User)
                .HasForeignKey<Marketer>(m => m.UserID)
                .OnDelete(DeleteBehavior.NoAction);
            // User => Cart
            builder.Entity<User>()
                 .HasMany(f => f.Carts)
                 .WithOne(u => u.User)
                 .HasForeignKey(f => f.UserID)
                 .OnDelete(DeleteBehavior.NoAction);

            //User => Order
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.NoAction);
            //////////////////////////////////////
            //Product => Favourite 
            builder.Entity<Product>()
                .HasMany(p => p.Favourites)
                .WithOne(f => f.Product)
                .HasForeignKey(p=>p.ProductID)
                .OnDelete(DeleteBehavior.NoAction);
            //Product => ProductImage
            builder.Entity<Product>()
               .HasMany(p => p.ProductImages)
               .WithOne(f => f.Product)
               .HasForeignKey(p => p.ProductID)
               .OnDelete(DeleteBehavior.NoAction);

            // Product => Rating
            builder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(f => f.Product)
                .HasForeignKey(p=>p.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            // Product => Cart
            builder.Entity<Cart>()
                .HasOne(p => p.Product)
                .WithMany(f => f.Carts)
                .HasForeignKey(f=>f.ProductID)
                .OnDelete(DeleteBehavior.NoAction);

            // Product => CollectionDetails
            builder.Entity<Product>()
                .HasMany(m => m.CollectionDetails)
                .WithOne(u => u.Product)
                .HasForeignKey(m => m.CollectionID)
                .OnDelete(DeleteBehavior.NoAction);

            //Product => Category
            builder.Entity<Product>()
                .HasOne(o => o.Category)
                .WithMany(o => o.Product)
                .HasForeignKey(o => o.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);

            //Product => Vendor
            builder.Entity<Vendor>()
                .HasMany(v => v.Product)
                .WithOne(p => p.Vendor)
                .HasForeignKey(p => p.VendorID)
                .OnDelete(DeleteBehavior.NoAction);

            //orderDetails => Product
            builder.Entity<OrderDetails>()
                .HasOne(o => o.Product)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o=>o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            ///////////////////////////////////////////////
            ///
            // Collection => Marketer
            builder.Entity<Collection>()
                .HasOne(m => m.Marketer)
                .WithMany(u => u.Collections)
                .HasForeignKey(m => m.MarketerID)
                .OnDelete(DeleteBehavior.NoAction);

            // Collection => CollectionDetails
            builder.Entity<CollectionDetails>()
                .HasOne(m => m.Collection)
                .WithMany(u => u.CollectionDetails)
                .HasForeignKey(m => m.CollectionID)
                .OnDelete(DeleteBehavior.NoAction);



            //order => OrderDetails
            builder.Entity<OrderDetails>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);


            //Driver => Order
            builder.Entity<Order>()
                .HasOne(o => o.Driver)
                .WithMany(o => o.Order)
                .HasForeignKey(o => o.DriverID)
                .OnDelete(DeleteBehavior.NoAction);

            //Driver => DriverPhone
            builder.Entity<PhoneDriver>()
                .HasOne(o => o.Driver)
                .WithMany(o => o.PhoneDriver)
                .HasForeignKey(o => o.DriverID)
                .OnDelete(DeleteBehavior.NoAction);


            //VendorImage => Vendor
            builder.Entity<Vendor>()
                .HasMany(v => v.VendorImage)
                .WithOne(v => v.Vendor)
                .HasForeignKey(p => p.VendorID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
