using ApiHakoBot.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Context
{
    public class HakoBotDbContext : DbContext
    {
        public HakoBotDbContext() : base ("name = HakoBotDbContext")
        {

        }
        public DbSet<Question> Questions { get; set; }
    }
}