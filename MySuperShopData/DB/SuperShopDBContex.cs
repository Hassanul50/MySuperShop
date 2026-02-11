using Microsoft.EntityFrameworkCore;
using MySuperShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperShopData.DB
{
    public class SuperShopDBContex:DbContext
    {
        public SuperShopDBContex(DbContextOptions<SuperShopDBContex> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}
