using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace extrade.models
{
    public class ExtradeContext : IdentityDbContext<User>
    {
        public ExtradeContext(DbContextOptions options):base(options) { }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<CollectionDetails> CollectionDetails { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorImage> VendorImgs { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<PhoneDriver> PhoneDrivers { get; set; }
        public DbSet<AllPayment> AllPayment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
            new categoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new CollectionConfiguration().Configure(modelBuilder.Entity<Collection>());
            new CollectionDetailsConfiguration().Configure(modelBuilder.Entity<CollectionDetails>());
            new driverConfiuration().Configure(modelBuilder.Entity<Driver>());
            new favouriteConfiguratin().Configure(modelBuilder.Entity<Favourite>());
            new MarketerConfiguration().Configure(modelBuilder.Entity<Marketer>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new orderdetailsConfiguration().Configure(modelBuilder.Entity<OrderDetails>());
            new PhoneConfiguration().Configure(modelBuilder.Entity<Phone>());
            new phonedriverConfiguration().Configure(modelBuilder.Entity<PhoneDriver>());
            new productConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProductImageConfiguration().Configure(modelBuilder.Entity<ProductImage>());
            new ratingConfiguration().Configure(modelBuilder.Entity<Rating>());
            new VendorConfiguration().Configure(modelBuilder.Entity<Vendor>());
            new VendorImageConfiguration().Configure(modelBuilder.Entity<VendorImage>());
            new AllPaymentConfiguration().Configure(modelBuilder.Entity<AllPayment>());
            
            base.OnModelCreating(modelBuilder);
        }


       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }














    }
}
