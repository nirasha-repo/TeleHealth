using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeleHealthDemo.Models;

namespace TeleHealthDemo.Data
{
    public class TeleHealthDBContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var path = "Data Source=" + Environment.CurrentDirectory + "\\Data\\telehealthdb.db";
                optionsBuilder.UseSqlite(path);                
            }
        }

    }
}
