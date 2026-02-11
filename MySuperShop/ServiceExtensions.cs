using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySuperShopData.DB;
using MySuperShopModel.DTOs;
using MySuperShopModel.Entities;
using System.Text;

namespace MySuperShop
{
    public static class ServiceExtensions
    {

        public static void AddMyServices(this IServiceCollection services, string connectionString)
        {
            // ------------------------
            // 1️⃣ Configure DbContext
            // ------------------------
            services.AddDbContext<SuperShopDBContex>(options =>
                options.UseSqlServer(connectionString));

            // ------------------------
            // 2️⃣ Configure Password Hasher
            // ------------------------
            services.AddScoped<IPasswordHasher<UserDTO>, PasswordHasher<UserDTO>>();

            // ------------------------
            // 3️⃣ Configure JWT Authentication
            // ------------------------
            var key = Encoding.ASCII.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // ------------------------
            // 4️⃣ Configure Controllers and JSON
            // ------------------------
            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // ------------------------
            // 5️⃣ Configure Swagger / OpenAPI
            // ------------------------
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // ------------------------
            // Optional: Add Authorization Policies
            // ------------------------
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "Admin"));
            //     options.AddPolicy("UserOnly", policy => policy.RequireClaim("Role", "User"));
            // });
        }


        //public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
        //{
        //    services.AddDbContext<SuperShopDBContex>(options =>
        //        options.UseSqlServer(connectionString));
        //}

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
    }
}
