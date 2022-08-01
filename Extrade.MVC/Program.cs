using System;
using extrade.models;
using Extrade.Repositories;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Extrade.MVC.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace Extrade.MVC
{

    public class program
    {
        public static int Main()
        {

            var builder = WebApplication.CreateBuilder();
            //       builder.Services.Configure<IdentityOptions>(options =>
            //options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            builder.Services.AddRazorPages();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTKey:Key"]))
                };
            });

            builder.Services.AddDbContext<ExtradeContext>(i =>
            {
                i.UseLazyLoadingProxies().UseSqlServer
                (builder.Configuration.GetConnectionString("Extrade"));
            });

            builder.Services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ExtradeContext>();
            builder.Services.Configure<IdentityOptions>(p =>
            {
                p.Password.RequiredLength = 6;
                p.Password.RequireDigit = false;
                p.Password.RequireUppercase = false;
                p.Password.RequiredUniqueChars = 0;
                p.Password.RequireNonAlphanumeric = false;
                p.Password.RequireLowercase = false;
                p.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                p.Lockout.MaxFailedAccessAttempts = 5;
            });

            builder.Services.AddScoped(typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(UserRepository));
            builder.Services.AddScoped(typeof(ProductRepository));
            builder.Services.AddScoped(typeof(MarketerRebository));
            builder.Services.AddScoped(typeof(VendorRepository));
            builder.Services.AddScoped(typeof(CategoryRepository));
            builder.Services.AddScoped(typeof(CollectionRepository));
            builder.Services.AddScoped(typeof(OrderRepository));
            builder.Services.AddScoped(typeof(OrderDetailsRepositoty));
            builder.Services.AddScoped(typeof(RoleRepository));
            builder.Services.AddScoped(typeof(PaymentRepository));
            builder.Services.AddScoped(typeof(CartRepository));
            builder.Services.AddScoped(typeof(FavouriteRepository));

            builder.Services.AddScoped(typeof(CollectionDetailsRepository));

            builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaims>();

            builder.Services.Configure<SignInOptions>(p =>
            {
                p.RequireConfirmedEmail = false;
            });
            builder.Services.ConfigureApplicationCookie(c =>
            {
                c.AccessDeniedPath = "/SignInMvc";
                c.LoginPath = "/SignInMvc";
            });
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddCors(i =>
            {
                i.AddDefaultPolicy(b =>
                {
                    b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                }
                );
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapDefaultControllerRoute();
            app.Run();
            return 0;
        }
    }
}