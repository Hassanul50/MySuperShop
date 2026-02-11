using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore; // required for UseSqlServer and AddDbContext extension
using Microsoft.IdentityModel.Tokens;
using MySuperShop;
using MySuperShopData.DB;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Get connection string from appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2️⃣ Call your new extension method
builder.Services.AddMyServices(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    //app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
