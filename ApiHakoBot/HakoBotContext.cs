using ApiHakoBot.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiHakoBot
{
    public class HakoBotContext : DbContext
    {

            public HakoBotContext() : base("name=HakoBotContext")
            {

            }

            public DbSet<Question> Questions { get; set; }

        
    }
}