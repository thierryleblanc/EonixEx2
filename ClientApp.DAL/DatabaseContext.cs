using ClientApp.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientApp.DAL
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}
